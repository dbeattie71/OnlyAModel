using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class GameOpenRequest
	{
		// TODO don't know what this is for. represent as a bool?
		public byte ConfirmUdp { get; }

		private GameOpenRequest(byte confirmUdp)
		{
			ConfirmUdp = confirmUdp;
		}

		[Unmarshaller(MessageType.Client.GameOpenRequest)]
		public static GameOpenRequest Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			byte confirmUdp = reader.ReadByte();
			return new GameOpenRequest(confirmUdp);
		}
	}
}
