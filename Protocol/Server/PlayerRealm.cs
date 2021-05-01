using Core;
using Protocol.Models;
using System;

namespace Protocol.Server
{
	public class PlayerRealm : ISendable
	{
		private readonly Realm _realm;

		public PlayerRealm(Realm realm)
		{
			_realm = realm;
		}

		public byte Type => MessageType.Server.PlayerRealm;

		public int Length(int protocolVersion) => 13;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			span[0] = (byte)_realm;
		}
	}
}
