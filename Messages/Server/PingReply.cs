using System;

namespace Messages.Server
{
	public class PingReply : IServerMessage
	{
		private readonly ushort _sequence;
		private readonly uint _timestamp;

		public PingReply(ushort sequence, uint timestamp)
		{
			_sequence = sequence;
			_timestamp = timestamp;
		}

		public byte Type => MessageType.Server.PingReply;

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
