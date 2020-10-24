using Core;
using System;

namespace Messages.ServerToClient
{
	public class CharacterOverview : IPayload
	{
		public MessageType.ServerToClient Type => MessageType.ServerToClient.CharacterOverview;

		public int Length => 10; // TODO real stuff

		public void Marshal(Span<byte> span)
		{
			// TODO real stuff - see DoL's PacketLib1125.SendCharacterOverview
		}
	}
}
