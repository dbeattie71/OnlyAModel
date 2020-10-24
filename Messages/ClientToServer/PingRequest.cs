using Core;
using System;

namespace Messages.ClientToServer
{
	public class PingRequest
	{
		/// <summary>
		/// Increases monotonically with each retry, in increments of
		/// approximately (and often exactly) 2^16. The client continues
		/// sending retries even after telling the user they've been
		/// disconnected.
		/// </summary>
		public uint Urgency { get; }
		/// <summary>
		/// Unknown unit and epoch. Increases monotonically with each ping,
		/// but remains stable between retries of the same ping.
		/// </summary>
		public uint Timestamp { get; }

		private PingRequest(uint urgency, uint timestamp)
		{
			Urgency = urgency;
			Timestamp = timestamp;
		}

		[AutowiredFactory(MessageType.ClientToServer.PingRequest)]
		public static PingRequest Unmarshall(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			var urgency = reader.ReadUInt32BigEndian();
			var timestamp = reader.ReadUInt32BigEndian();
			return new PingRequest(urgency, timestamp);
		}
	}
}
