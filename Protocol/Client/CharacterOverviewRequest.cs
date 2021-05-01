using Core.Event;
using Protocol.Autowire;
using Protocol.Models;

namespace Protocol.Client
{
	public class CharacterOverviewRequest
	{
		public Realm Realm { get; }

		public CharacterOverviewRequest(Realm realm)
		{
			Realm = realm;
		}

		[Unmarshaller(MessageType.Client.CharacterOverviewRequest)]
		public static CharacterOverviewRequest Unmarshal(MessageEventArgs args)
		{
			var realm = (Realm)args.Message.Payload.Span[0];
			return new CharacterOverviewRequest(realm);
		}
	}
}
