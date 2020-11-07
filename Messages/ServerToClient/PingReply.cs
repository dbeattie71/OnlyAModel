using System;

namespace Messages.ServerToClient
{
	public class PingReply : IPayload
	{
		private readonly ushort _sequence;
		private readonly uint _timestamp;

		public PingReply(ushort sequence, uint timestamp)
		{
			_sequence = sequence;
			_timestamp = timestamp;
		}

		public byte MessageType => Messages.MessageType.ServerToClient.PingReply;

		public int Length => 16;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.WriteUInt32BigEndian(_timestamp);
			writer.Skip(4);
			writer.WriteUInt16BigEndian(_sequence); // DoL sends _sequence + 1 for some reason
		}
	}
}
