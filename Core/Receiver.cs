using Core.Data;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Core
{
	internal class Receiver
	{
		private readonly Server _server;
		private readonly Session _session;
		private readonly Socket _socket;
		private readonly MessageBuffer _buffer = new MessageBuffer(2048);

		internal Receiver(Server server, Session session, Socket socket)
		{
			_server = server;
			_session = session;
			_socket = socket;
		}

		internal void Start()
		{
			var args = new SocketAsyncEventArgs();
			args.SetBuffer(new byte[2048]);
			args.Completed += ReceiveComplete;
			BeginReceive(_socket, args);
		}

		private void BeginReceive(Socket socket, SocketAsyncEventArgs args)
		{
			try
			{
				if (!socket.ReceiveAsync(args))
				{
					ReceiveComplete(socket, args);
				}
			}
			catch (Exception e)
			{
				_server.RaiseError(_session, e);
				args.Dispose();
			}
		}

		private void ReceiveComplete(object socket, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				if (args.BytesTransferred == 0)
				{
					_server.RaiseConnect(_session, false);
					args.Dispose();
				}
				else
				{
					BufferMessages(args.MemoryBuffer.Slice(0, args.BytesTransferred));
					BeginReceive((Socket)socket, args);
				}
			}
			else
			{
				var e = new Exception(args.SocketError.ToString());
				_server.RaiseError(_session, e);
				args.Dispose();
			}
		}

		private void BufferMessages(Memory<byte> bytes)
		{
			_buffer.Append(bytes);
			while (_buffer.TryGetMessage(out Message message))
			{
				if (_session.Version.MajorVersion == 0)
				{
					_session.Version = MemoryMarshal.Read<ClientVersion>(message.Payload.Span);
				}
				_server.RaiseMessageReceived(_session, message);
			}
		}
	}
}
