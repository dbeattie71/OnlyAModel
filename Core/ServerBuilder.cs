using Core.Event;
using System;

namespace Core
{
	public class ServerBuilder
	{
		public ServerConfig Config { get; } = new ServerConfig();
		public event EventHandler<ConnectionEventArgs> OnConnection;
		public event EventHandler<MessageEventArgs> OnMessageSent;
		public event EventHandler<MessageEventArgs> OnMessageReceived;
		public event EventHandler<ErrorEventArgs> OnError;

		public void Add(IServerObserver observer)
		{
			OnConnection += observer.OnConnection;
			OnMessageSent += observer.OnMessageSent;
			OnMessageReceived += observer.OnMessageReceived;
			OnError += observer.OnError;
		}

		public Server Build()
		{
			return new Server(Config, OnConnection, OnMessageSent, OnMessageReceived, OnError);
		}
	}
}
