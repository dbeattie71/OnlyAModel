using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

namespace Core
{
	internal class Sender
	{
		private readonly Server _server;
		private readonly Session _session;
		private readonly Socket _socket;

		private readonly ConcurrentQueue<IMessage> _queue = new ConcurrentQueue<IMessage>();
		private int _sending = 0;

		internal Sender(Server server, Session session, Socket socket)
		{
			_server = server;
			_session = session;
			_socket = socket;
		}

		internal void Send(IMessage message)
		{
			_queue.Enqueue(message);
			if (Interlocked.Increment(ref _sending) == 1)
			{
				var args = new SocketAsyncEventArgs();
				args.Completed += SendComplete;
				SendNext(args);
			}
		}

		private void SendNext(SocketAsyncEventArgs args)
		{
			if (_queue.TryPeek(out IMessage message))
			{
				args.SetBuffer(message.Data.ToArray());
				BeginSend(args);
			}
			else
			{
				// should be impossible
				var e = new Exception("No message to send!");
				_server.RaiseError(_session, e);
				// TODO reset lock?
			}
		}

		private void BeginSend(SocketAsyncEventArgs args)
		{
			try
			{
				if (!_socket.SendAsync(args))
				{
					SendComplete(_socket, args);
				}
			}
			catch (Exception e)
			{
				_server.RaiseError(_session, e);
				args.Dispose();
				// TODO reset lock?
			}
		}
		private void SendComplete(object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				if (args.BytesTransferred == args.MemoryBuffer.Length)
				{
					_queue.TryDequeue(out IMessage message);
					_server.RaiseMessageSent(_session, message);
					if (Interlocked.Decrement(ref _sending) > 0)
					{
						SendNext(args);
					}
					else
					{
						args.Dispose();
					}
				}
				else
				{
					// in blocking mode, which is the default, we should not get a
					// success callback until all bytes have been transferred.
					// unsure if we'll ever get zero indicating a closed connection.
					// TODO if this condition is never reached, BeginSend can be merged into SendNext
					var msg = string.Format("unexpected send callback with {0} of {1} bytes transferred", args.MemoryBuffer.Length, args.BytesTransferred);
					var e = new Exception(msg);
					_server.RaiseError(_session, e);
					args.Dispose();
					//var remaining = args.MemoryBuffer.Length - args.BytesTransferred;
					//args.SetBuffer(args.MemoryBuffer.Slice(args.BytesTransferred, remaining));
					//BeginSend(args);
				}
			}
			else
			{
				var e = new Exception(args.SocketError.ToString());
				_server.RaiseError(_session, e);
				args.Dispose();
				// TODO reset lock?
			}
		}
	}
}
