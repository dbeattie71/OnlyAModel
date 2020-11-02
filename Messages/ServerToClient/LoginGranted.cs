﻿using Core;
using Models.World;
using System;

namespace Messages.ServerToClient
{
	public class LoginGranted : IPayload
	{
		private readonly string _user;
		private readonly string _serverName;
		private readonly byte _serverId;
		private readonly PvPMode _pvpMode;
		private readonly bool _trialAccount;

		public LoginGranted(string user, string serverName, PvPMode pvpMode)
		{
			_user = user;
			_serverName = serverName;
			_serverId = 0x01;
			_pvpMode = pvpMode;
			_trialAccount = false;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.LoginGranted;

		public int Length => _user.Length + _serverName.Length + 13;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.WriteShortString(_user);
			writer.WriteShortString(_serverName);
			writer.WriteByte(_serverId);
			writer.WriteByte((byte)_pvpMode);
			writer.WriteByte((byte)(_trialAccount ? 1 : 0));
		}
	}
}
