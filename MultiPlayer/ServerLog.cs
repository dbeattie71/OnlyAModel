using Core;
using Core.Event;
using Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MultiPlayer
{
	public class ServerLog : IServerObserver
	{
		private readonly ILogger<ServerLog> _log;

		public ServerLog(ILogger<ServerLog> log)
		{
			_log = log;
		}

		public void OnConnection(object server, ConnectionEventArgs args)
		{
			if (args.Connected)
			{
				_log.LogDebug("Session {0} connected from {1}", args.Session.Id, args.Session.RemoteEndPoint);
			}
			else
			{
				_log.LogDebug("Session {0} disconnected", args.Session.Id);
			}
		}

		public void OnMessageSent(object server, MessageEventArgs args)
		{
			if (args.Message.Type != MessageType.ServerToClient.PingReply && _log.IsEnabled(LogLevel.Debug))
			{
				var type = MessageType.ServerToClient.GetName(args.Message.Type, args.Session.Version.ProtocolVersion) ?? "unknown";
				_log.LogDebug("Session {0} <= {1}", args.Session.Id, type);
				var arr = args.Message.Data.ToArray();
				_log.LogDebug(BitConverter.ToString(arr).Replace("-", " "));
				_log.LogDebug(string.Concat(arr.Select(b => b >= 32 && b <= 126 ? (char)b + "  " : ".  ")));
			}
		}

		public void OnMessageReceived(object server, MessageEventArgs args)
		{
			if (args.Message.Type != MessageType.ClientToServer.PingRequest && _log.IsEnabled(LogLevel.Debug))
			{
				var type = MessageType.ClientToServer.GetName(args.Message.Type, args.Session.Version.ProtocolVersion) ?? "unknown";
				_log.LogDebug("Session {0} => {1}", args.Session.Id, type);
				var arr = args.Message.Data.ToArray();
				_log.LogDebug(BitConverter.ToString(arr).Replace("-", " "));
				_log.LogDebug(string.Concat(arr.Select(b => b >= 32 && b <= 126 ? (char)b + "  " : ".  ")));
			}
		}

		public void OnError(object server, ErrorEventArgs args)
		{
			if (args.Session == null)
			{
				_log.LogError("Server error: {0}", args.Error.Message);
			}
			else
			{
				_log.LogError("Session {0} error: {1}", args.Session.Id, args.Error.Message);
			}
		}
	}
}
