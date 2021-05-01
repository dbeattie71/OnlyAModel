using Core;
using Core.Event;
using Protocol.Autowire;
using System;

namespace Demo
{
	class Program
	{
		static void Main()
		{
			var builder = new ServerBuilder();
			builder.Add(new Logger());
			builder.OnMessageReceived += OnMessageReceived;
			var server = builder.Build();
			using var start = server.Start();
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}

		private static void OnMessageReceived(object server, MessageEventArgs args)
		{
			if (args.Session.Data == null)
			{
				var handlers = new object[] { new Handlers() };
				args.Session.Data = Autowire.CreateHandler(handlers);
			}
			((EventHandler<MessageEventArgs>)args.Session.Data)(server, args);
		}
	}
}
