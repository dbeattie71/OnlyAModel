using Messages.Models;
using System;

namespace Messages.Server
{
	public class CharacterPoints : IServerMessage
	{
		private Points _points;

		public CharacterPoints(Points points)
		{
			_points = points;
		}

		public byte Type => Messages.MessageType.Server.CharacterPoints;

		public int Length => System.Runtime.InteropServices.Marshal.SizeOf(typeof(Points));

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.Write(_points);
		}
	}
}
