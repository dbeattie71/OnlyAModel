using Core;
using System;

namespace Messages.ServerToClient
{
	public class LoginGranted : IPayload
	{
		private readonly string _user;
		private readonly string _serverName;
		// DoL hardcodes 0x05, notes say seems irrelevant
		private readonly byte _serverId;
		// DoL notes say 00 normal type?, 01 mordred type, 03 gaheris type, 07 ywain type
		// not sure why this is called color - maybe refers to RvR modes?
		private readonly byte _serverColor;
		private readonly bool _trialAccount;

		public LoginGranted(string user, string serverName)
		{
			_user = user;
			_serverName = serverName;
			_serverId = 0x00;
			_serverColor = 0x00;
			_trialAccount = false;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.LoginGranted;

		public int Length => _user.Length + _serverName.Length + 13;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.WriteDaocString(_user);
			writer.WriteDaocString(_serverName);
			writer.WriteByte(_serverId);
			writer.WriteByte(_serverColor);
			writer.WriteByte(_trialAccount ? (byte)0x01 : (byte)0x00);
		}
	}
}
