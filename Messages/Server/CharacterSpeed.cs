using System;

namespace Messages.Server
{
	public class CharacterSpeed : IServerMessage
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

		public byte Type => MessageType.Server.CharacterSpeed;

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
