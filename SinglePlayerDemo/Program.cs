using Core;
using Core.Event;
using Messages;
using System;

namespace SinglePlayerDemo
{
	class Program
	{
		static void Main()
		{
			var builder = new ServerBuilder();
			builder.Add(new ConsoleLogger());
			builder.OnMessageReceived += OnMessageReceived;
			var server = builder.Build();
			using(var start = server.Start())
			{
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
			}
		}

		private static void OnMessageReceived(object server, MessageEventArgs args)
		{
			if(args.Session.UserData == null)
			{
				args.Session.UserData = Autowire.CreateHandler(args.Session.Version.ProtocolVersion, new SessionHandler());
			}
			((EventHandler<MessageEventArgs>)args.Session.UserData)(server, args);
		}
	}
}
