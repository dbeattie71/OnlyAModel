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
			if(args.Session.Data == null)
			{
				args.Session.Data = Autowire.CreateMessageHandler(args.Session.Version.ProtocolVersion, new SessionHandler());
			}
			((EventHandler<MessageEventArgs>)args.Session.Data)(server, args);
		}
	}
}
