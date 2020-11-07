using System;

namespace Messages.ServerToClient
{
	public class CharacterSpeed : IPayload
	{
		private readonly ushort _landSpeed;
		private readonly byte _waterSpeed;
		private readonly byte _turningDisabled;

		/// <summary>
		/// Land speed is a percentage of default speed. Water speed is a
		/// percentage of land speed.
		/// </summary>
		public CharacterSpeed(ushort landSpeed, byte waterSpeed, bool turningDisabled)
		{
			_landSpeed = landSpeed;
			_waterSpeed = waterSpeed;
			_turningDisabled = (byte)(turningDisabled ? 0x01 : 0x00);
		}

		public byte MessageType => Messages.MessageType.ServerToClient.CharacterSpeed;

		public int Length => 4;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.WriteUInt16BigEndian(_landSpeed);
			writer.WriteByte(_turningDisabled);
			writer.WriteByte(_waterSpeed);
		}
	}
}
