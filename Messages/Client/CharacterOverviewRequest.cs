using Messages.Models;
using System;

namespace Messages.Client
{
	public class CharacterOverviewRequest
	{
		public Realm Realm { get; }

		public CharacterOverviewRequest(Realm realm)
		{
			Realm = realm;
		}

		[AutowiredFactory(MessageType.Client.CharacterOverviewRequest)]
		public static CharacterOverviewRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			var realm = (Realm)payload.Span[0];
			return new CharacterOverviewRequest(realm);
		}
	}
}
