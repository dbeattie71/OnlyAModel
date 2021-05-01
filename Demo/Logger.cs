using Core;
using Core.Event;
using Protocol;
using System;
using System.Linq;

namespace Demo
{
	class Logger : IServerObserver
	{
		public void OnConnection(object server, ConnectionEventArgs args)
		{
			if (args.Connected)
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
			if (args.Message.Type != MessageType.Server.PingReply)
			{
				var type = MessageType.Server.GetName(args.Message.Type, args.Session.ProtocolVersion) ?? "unknown";
				Console.WriteLine("Session {0} <= {1}", args.Session.Id, type);

				var arr = args.Message.Data.ToArray();
				Console.WriteLine(BitConverter.ToString(arr).Replace("-", " "));
				Console.WriteLine(string.Concat(arr.Select(b => b >= 32 && b <= 126 ? (char)b + "  " : ".  ")));
			}
		}

		public void OnMessageReceived(object server, MessageEventArgs args)
		{
			if (args.Message.Type != MessageType.Client.PingRequest)
			{
				var type = MessageType.Client.GetName(args.Message.Type, args.Session.ProtocolVersion) ?? "unknown";
				Console.WriteLine("Session {0} => {1}", args.Session.Id, type);

				var arr = args.Message.Data.ToArray();
				Console.WriteLine(BitConverter.ToString(arr).Replace("-", " "));
				Console.WriteLine(string.Concat(arr.Select(b => b >= 32 && b <= 126 ? (char)b + "  " : ".  ")));
			}
		}

		public void OnError(object server, ErrorEventArgs args)
		{
			if (args.Session == null)
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
