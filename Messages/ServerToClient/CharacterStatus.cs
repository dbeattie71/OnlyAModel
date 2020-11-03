using Core;
using Models.Character;
using System;

namespace Messages.ServerToClient
{
	public class CharacterStatus : IPayload
	{
		private Character _character;

		public CharacterStatus(Character character)
		{
			_character = character;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.CharacterStatus;

		public int Length => 22;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			var status = _character.Status;
			writer.WriteByte((byte)(status.Health * 100 / status.MaxHealth));
			writer.WriteByte((byte)(status.Mana * 100 / status.MaxMana));
			writer.WriteByte((byte)(_character.Sitting ? 0x02 : 0x00));
			writer.WriteByte((byte)(status.Endurance * 100 / status.MaxEndurance));
			writer.WriteByte((byte)(status.Concentration * 100 / status.MaxConcentration));
			// DoL represents "alive" as a separate property, but elsewhere uses health > 0
			// DoL hard codes 0x00 for alive - some doubt in comments about how to represent dead
			writer.WriteByte((byte)(status.Health > 0 ? 0x00 : 0x0F));
			writer.Write(status);
		}
	}
}
