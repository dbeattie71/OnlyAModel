using Core.Event;
using System;
using System.Net;
using System.Net.Sockets;

namespace Core
{
	public class Server
	{
		private ServerConfig Config { get; }
		private EventHandler<ConnectionEventArgs> OnConnect { get; }
		private EventHandler<MessageEventArgs> OnMessageSent { get; }
		private EventHandler<MessageEventArgs> OnMessageReceived { get; }
		private EventHandler<ErrorEventArgs> OnError { get; }

		internal Server(
			ServerConfig config,
			EventHandler<ConnectionEventArgs> onConnect,
			EventHandler<MessageEventArgs> onMessageSent,
			EventHandler<MessageEventArgs> onMessageReceived,
			EventHandler<ErrorEventArgs> onError)
		{
			Config = new ServerConfig(config);
			OnConnect = onConnect;
			OnMessageSent = onMessageSent;
			OnMessageReceived = onMessageReceived;
			OnError = onError;
		}

		public IDisposable Start()
		{
			var disposables = new CompositeDisposable();
			var socket = new Socket(Config.Address.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);
			var endpoint = new IPEndPoint(Config.Address, Config.Port);
			socket.Bind(endpoint);
			socket.Listen(Config.Backlog);
			for(int i = 0; i < Config.Threads; ++i)
			{
				var args = new SocketAsyncEventArgs() { UserToken = disposables };
				args.Completed += AcceptComplete;
				BeginAccept(socket, args);
			}
			disposables.Add(socket);
			return disposables;
		}

		private void BeginAccept(Socket socket, SocketAsyncEventArgs args)
		{
			try
			{
				if(!socket.AcceptAsync(args))
				{
					AcceptComplete(socket, args);
				}
			}
			catch(Exception e)
			{
				RaiseError(null, e);
			}
		}

		private void AcceptComplete(object sender, SocketAsyncEventArgs args)
		{
			if(args.SocketError == SocketError.Success)
			{
				var disposables = (CompositeDisposable)args.UserToken;
				disposables.Add(args.AcceptSocket);
				var session = new Session(this, args.AcceptSocket, () => disposables.Remove(args.AcceptSocket));
				RaiseConnect(session, true);
				session.Start();
				args.AcceptSocket = null;
				BeginAccept((Socket)sender, args);
			}
			else if(args.SocketError == SocketError.ConnectionReset)
			{
				BeginAccept((Socket)sender, args);
			}
			else
			{
				var e = new Exception(args.SocketError.ToString());
				RaiseError(null, e);
			}
		}

		internal void RaiseConnect(Session session, bool connected)
		{
			var evt = new ConnectionEventArgs(session, connected);
			OnConnect?.Invoke(this, evt);
		}

		internal void RaiseMessageSent(Session session, IMessage message)
		{
			var evt = new MessageEventArgs(session, message);
			OnMessageSent?.Invoke(this, evt);
		}

		internal void RaiseMessageReceived(Session session, IMessage message)
		{
			var evt = new MessageEventArgs(session, message);
			OnMessageReceived?.Invoke(this, evt);
		}

		internal void RaiseError(Session session, Exception error)
		{
			var evt = new ErrorEventArgs(session, error);
			OnError?.Invoke(this, evt);
		}
	}
}
