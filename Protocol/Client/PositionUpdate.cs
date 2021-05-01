using Core.Event;
using Protocol.Autowire;
using Protocol.Models;

namespace Protocol.Client
{
	public class PositionUpdate
	{
		public readonly Coordinates Coordinates;

		private PositionUpdate(Coordinates coordinates)
		{
			Coordinates = coordinates;
		}

		[Unmarshaller(MessageType.Client.PositionUpdate)]
		public static PositionUpdate Unmarshal(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var x = reader.ReadFloat();
			var y = reader.ReadFloat();
			var z = reader.ReadFloat();
			// lots of other information
			// see DoL's PlayerPositionUpdateHandler.cs
			reader.Skip(16);
			var heading = reader.ReadUInt16LittleEndian();
			var coordinates = new Coordinates(x, y, z, heading);
			return new PositionUpdate(coordinates);
		}
	}
}
