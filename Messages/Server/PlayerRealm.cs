using Messages.Models;
using System;

namespace Messages.Server
{
	public class PlayerRealm : IServerMessage
	{
		private readonly Realm _realm;

		public PlayerRealm(Realm realm)
		{
			_realm = realm;
		}

		public byte Type => MessageType.Server.PlayerRealm;

		public int Length => 13;

		public void Marshal(Span<byte> span)
		{
			span[0] = (byte)_realm;
		}
	}
}
