using Core.Event;
using Protocol.Autowire;
using System;

namespace Protocol.Client
{
	public class Ping
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

		private Ping(uint urgency, uint timestamp)
		{
			Urgency = urgency;
			Timestamp = timestamp;
		}

		[Unmarshaller(MessageType.Client.Ping)]
		public static Ping Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var urgency = reader.ReadUInt32BigEndian();
			var timestamp = reader.ReadUInt32BigEndian();
			return new Ping(urgency, timestamp);
		}
	}
}
