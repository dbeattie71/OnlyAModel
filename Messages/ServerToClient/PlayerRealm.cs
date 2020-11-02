using Core;
using Models.Character;
using System;

namespace Messages.ServerToClient
{
	public class PlayerRealm : IPayload
	{
		private readonly Realm _realm;

		public PlayerRealm(Realm realm)
		{
			_realm = realm;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.PlayerRealm;

		public int Length => 13;

		public void Marshal(Span<byte> span)
		{
			span[0] = (byte)_realm;
		}
	}
}
