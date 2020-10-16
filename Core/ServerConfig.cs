using System.Net;

namespace Core
{
	public class ServerConfig
	{
		public IPAddress Address { get; set; }
		public int Port { get; set; }
		public int Backlog { get; set; }
		public int Threads { get; set; }

		public ServerConfig()
		{
			Address = IPAddress.Loopback;
			Port = 13013;
			Backlog = 10;
			Threads = 1;
		}

		public ServerConfig(ServerConfig other)
		{
			Address = other.Address;
			Port = other.Port;
			Backlog = other.Backlog;
			Threads = other.Threads;
		}
	}
}
