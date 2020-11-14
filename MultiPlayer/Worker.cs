using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MultiPlayer
{
	public class Worker : BackgroundService
	{
		private readonly ILogger _log;
		private readonly Server _server;
		private readonly IHostApplicationLifetime _app;

		public Worker(ILogger<Worker> log, Server server, IHostApplicationLifetime app)
		{
			_log = log;
			_server = server;
			_app = app;
		}

		protected override async Task ExecuteAsync(CancellationToken cancel)
		{
#if DEBUG
			Thread t = new Thread(() =>
			{
				System.Console.ReadLine();
				_app.StopApplication();
			});
			t.Start();
			_log.LogDebug("Press enter to exit.");
#endif
			try
			{
				_log.LogInformation("Server started.");
				using var start = _server.Start();
				await Task.Delay(Timeout.Infinite, cancel);
			}
			catch (TaskCanceledException)
			{
				_log.LogInformation("Server stopped.");
			}
		}
	}
}
