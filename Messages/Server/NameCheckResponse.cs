using Messages.Models;
using System;

namespace Messages.Server
{
	public class NameCheckResponse : IServerMessage
	{
		private readonly string _name;
		private readonly string _user;
		private readonly NameStatus _status;

		public NameCheckResponse(string name, string user, NameStatus status)
		{
			_name = name;
			_user = user;
			_status = status;
		}

		public byte Type => MessageType.Server.NameCheckResponse;

		public int Length => 58;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.WriteFixedString(_name, 30);
			writer.WriteFixedString(_user, 24);
			writer.WriteByte((byte)_status);
			// 3 unknown bytes
		}
	}
}
