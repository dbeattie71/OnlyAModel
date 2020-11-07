using Models.World;
using System;

namespace Messages.ClientToServer
{
	public class PositionUpdate
	{
		public readonly Coordinates Coordinates;

		private PositionUpdate(Coordinates coordinates)
		{
			Coordinates = coordinates;
		}

		[AutowiredFactory(MessageType.ClientToServer.PositionUpdate)]
		public static PositionUpdate Unmarshal(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
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
