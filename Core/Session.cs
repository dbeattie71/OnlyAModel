using Core.Data;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Core
{
	public class Session : IDisposable
	{
		private static long _nextId = 0;
		public long Id { get; } = Interlocked.Increment(ref _nextId);
		public IPEndPoint RemoteEndPoint { get => (IPEndPoint)_socket.RemoteEndPoint; }
		public IPEndPoint LocalEndPoint { get => (IPEndPoint)_socket.LocalEndPoint; }
		/// <summary>
		/// Not initialized until the first message is received.
		/// </summary>
		public ClientInfo ClientInfo { get; internal set; }
		/// <summary>
		/// Not initialized until the first message is received.
		/// </summary>
		public int ProtocolVersion { get; internal set; }
		/// <summary>
		/// Application data associated with this session.
		/// </summary>
		public object Data { get; set; }

		private readonly Socket _socket;
		private readonly Sender _sender;
		private readonly Receiver _receiver;
		private readonly Action _onDispose;

		internal Session(Server server, Socket socket, Action onDispose)
		{
			_socket = socket;
			_sender = new Sender(server, this, socket);
			_receiver = new Receiver(server, this, socket);
			_onDispose = onDispose;
		}

		internal void Start()
		{
			_receiver.Start();
		}

		public void Send(ISendable sendable)
		{
			var message = new TcpMessage(sendable, ProtocolVersion);
			_sender.Send(message);
		}

		public void Dispose()
		{
			_onDispose();
		}
	}
}
