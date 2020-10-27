using Core;
using Messages.Models;
using System;

namespace Messages.ClientToServer
{
	public class CharacterOverviewRequest
	{
		public Realm Realm { get; }

		private CharacterOverviewRequest(Realm realm)
		{
			Realm = realm;
		}

		[AutowiredFactory(MessageType.ClientToServer.CharacterOverviewRequest)]
		public static CharacterOverviewRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			var realm = (Realm)payload.Span[0];
			return new CharacterOverviewRequest(realm);
		}
	}
}
