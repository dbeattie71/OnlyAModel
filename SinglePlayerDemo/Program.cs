using Core;
using System;

namespace SinglePlayerDemo
{
	class Program
	{
		static void Main()
		{
			var builder = new ServerBuilder();
			var handlers = new ClientHandlers();
			builder.OnMessageReceived += handlers.OnMessageReceived;
			builder.Add(new ConsoleLogger());
			var server = builder.Build();
			using(var start = server.Start())
			{
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
			}
		}
	}
}
