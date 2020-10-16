using Core;
using Core.Event;
using Messages;
using System;

namespace SinglePlayerDemo
{
	class ConsoleLogger : IServerObserver
	{
		public void OnConnection(object server, ConnectionEventArgs args)
		{
			if(args.Connected)
			{
				Console.WriteLine("Session {0} connected from {1}", args.Session.Id, args.Session.RemoteEndPoint);
			}
			else
			{
				Console.WriteLine("Session {0} disconnected", args.Session.Id);
			}
			
		}

		public void OnMessageSent(object server, MessageEventArgs args)
		{
			var type = Enum.GetName(typeof(MessageType.ServerToClient), args.Message.Type) ?? "unknown";
			Console.WriteLine("Session {0} <= {1}", args.Session.Id, type);
		}

		public void OnMessageReceived(object server, MessageEventArgs args)
		{
			var type = Enum.GetName(typeof(MessageType.ClientToServer), args.Message.Type) ?? "unknown";
			Console.WriteLine("Session {0} => {1}", args.Session.Id, type);
		}

		public void OnError(object server, ErrorEventArgs args)
		{
			if(args.Session == null)
			{
				Console.Error.WriteLine("Server error: {0}", args.Error.Message);
			}
			else
			{
				Console.Error.WriteLine("Session {0} error: {1}", args.Session.Id, args.Error.Message);
			}
		}
	}
}
