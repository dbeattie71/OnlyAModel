using Core.Data;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Core
{
	public class Session : IDisposable
	{
		private static long _nextId = 0;
		public long Id { get; } = Interlocked.Increment(ref _nextId);
		private Server Server { get; }
		private Socket Socket { get; }
		public EndPoint RemoteEndPoint { get { return Socket.RemoteEndPoint; } }
		/// <summary>
		/// Not initialized until the first message is received.
		/// </summary>
		public ClientVersion Version { get; private set; }
		private MessageBuffer Buffer { get; } = new MessageBuffer(2048);
		public object UserData { get; set; }

		internal Session(Server server, Socket socket)
		{
			Server = server;
			Socket = socket;
		}

		// TODO make Start public so observer can decide if errors are fatal?
		internal void Start()
		{
			var args = new SocketAsyncEventArgs();
			args.SetBuffer(new byte[2048]);
			args.Completed += ReceiveComplete;
			BeginReceive(Socket, args);
		}

		private void BeginReceive(Socket socket, SocketAsyncEventArgs args)
		{
			try
			{
				if(!socket.ReceiveAsync(args))
				{
					ReceiveComplete(socket, args);
				}
			}
			catch(Exception e)
			{
				Server.RaiseError(this, e);
				args.Dispose();
			}
		}

		private void ReceiveComplete(object socket, SocketAsyncEventArgs args)
		{
			if(args.SocketError == SocketError.Success)
			{
				if(args.BytesTransferred == 0)
				{
					Server.RaiseConnect(this, false);
					args.Dispose();
				}
				else
				{
					ReadMessages(args.MemoryBuffer.Slice(0, args.BytesTransferred));
					BeginReceive((Socket)socket, args);
				}
			}
			else
			{
				var e = new Exception(args.SocketError.ToString());
				Server.RaiseError(this, e);
				args.Dispose();
			}
		}

		private void ReadMessages(Memory<byte> bytes)
		{
			Buffer.Append(bytes);
			while(Buffer.TryGetMessage(out Message message))
			{
				if(Version.MajorVersion == 0)
				{
					Version = MemoryMarshal.Read<ClientVersion>(message.Payload.Span);
				}
				Server.RaiseMessageReceived(this, message);
			}
		}

		public void Dispose()
		{
			Socket.Dispose();
		}
	}
}
