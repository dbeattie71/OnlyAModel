using Core;
using Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiPlayer.Handlers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPlayer
{
	public class Program
	{
		public static void Main()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(config)
				.CreateLogger();
			try
			{
				CreateHost().Run();
			}
			catch (Exception e)
			{
				Log.Fatal(e, "Something went wrong.");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static IHost CreateHost() =>
			Host.CreateDefaultBuilder()
				.UseSerilog()
				.UseWindowsService()
				.ConfigureServices((context, services) =>
				{
					services.AddSingleton<ServerLog>();
					services.AddSingleton<IHandler, PingHandler>();
					services.AddSingleton<IHandler, HandshakeHandler>();
					services.AddSingleton<IHandler, AuthHandler>();
					services.AddSingleton<IHandler, CharSelectHandler>();
					services.AddSingleton<IHandler, WorldEntryHandler>();
					services.AddSingleton<IHandler, PositionHandler>();
					services.AddSingleton<IHandler, CommandHandler>();
					// TODO all the handlers!
					// if any handlers are stateful, make them transient
					services.AddSingleton((sp) => CreateServer(sp));
					services.AddHostedService<Worker>();
				})
				.Build();

		private static Server CreateServer(IServiceProvider sp)
		{
			var builder = new ServerBuilder();
			builder.Add(sp.GetRequiredService<ServerLog>());
			builder.OnMessageReceived += (server, args) =>
			{
				if(args.Session.GetState() == null)
				{
					var handlers = sp.GetRequiredService<IEnumerable<IHandler>>().ToArray();
					var handler = Autowire.CreateMessageHandler(args.Session.Version.ProtocolVersion, handlers);
					args.Session.SetState(new SessionState(handler));
				}
				args.Session.GetState().OnMessageReceived(server, args);
			};
			return builder.Build();
		}
	}
}
