using Core;
using Protocol.Models;
using System;

namespace Protocol.Server
{
	public class CharacterPoints : ISendable
	{
		private Points _points;

		public CharacterPoints(Points points)
		{
			_points = points;
		}

		public byte Type => MessageType.Server.CharacterPoints;

		public int Length(int protocolVersion) => System.Runtime.InteropServices.Marshal.SizeOf(typeof(Points));

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			var writer = new SpanWriter(span);
			writer.Write(_points);
		}
	}
}
