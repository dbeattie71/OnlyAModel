using Models.Character;
using System;

namespace Messages.ServerToClient
{
	public class CharacterPoints : IPayload
	{
		private Points _points;

		public CharacterPoints(Points points)
		{
			_points = points;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.CharacterPoints;

		public int Length => System.Runtime.InteropServices.Marshal.SizeOf(typeof(Points));

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.Write(_points);
		}
	}
}
