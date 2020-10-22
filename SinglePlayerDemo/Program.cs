using Core;
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
			var handlers = new AutowiredHandlers();
			builder.OnMessageReceived += Autowire.CreateHandler(1125, handlers);
			var server = builder.Build();
			using(var start = server.Start())
			{
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
			}
		}
	}
}
