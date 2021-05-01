using Core;
using System;

namespace Protocol.Server
{
	public class PingResponse : ISendable
	{
		private readonly ushort _sequence;
		private readonly uint _timestamp;

		public PingResponse(ushort sequence, uint timestamp)
		{
			_sequence = sequence;
			_timestamp = timestamp;
		}

		public byte Type => MessageType.Server.PingResponse;

		public int Length(int protocolVersion) => 16;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			var writer = new SpanWriter(span);
			writer.WriteUInt32BigEndian(_timestamp);
			writer.Skip(4);
			writer.WriteUInt16BigEndian(_sequence);
		}
	}
}
