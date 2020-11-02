using System.Net;

namespace Models.World
{
	/// <summary>
	/// To the player, a region is a set of contiguous zones they can walk
	/// between without a loading screen. Things like realm association and
	/// water level are properties of regions. Region IDs also seem to control
	/// the in-game map, which implies they're baked into the client. The
	/// protocol suggests that each region can be handled by its own server.
	/// </summary>
	public class Region
	{
		public ushort Id { get; }
		public IPAddress Address { get; }
		public int Port { get; }

		public Region(ushort id, IPAddress address, int port)
		{
			Id = id;
			Address = address;
			Port = port;
		}
	}
}
