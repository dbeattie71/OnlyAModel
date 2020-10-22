using Core.Data;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Core
{
	public class Session : IDisposable
	{
		private static long _nextId = 0;
		public long Id { get; } = Interlocked.Increment(ref _nextId);
		public EndPoint RemoteEndPoint { get { return _socket.RemoteEndPoint; } }
		/// <summary>
		/// Not initialized until the first message is received.
		/// </summary>
		public ClientVersion Version { get; internal set; }
		public object UserData { get; set; }

		private readonly Socket _socket;
		private readonly Sender _sender;
		private readonly Receiver _receiver;

		internal Session(Server server, Socket socket)
		{
			_socket = socket;
			_sender = new Sender(server, this, socket);
			_receiver = new Receiver(server, this, socket);
		}

		internal void Start()
		{
			_receiver.Start();
		}

		public void Send(IPayload payload)
		{
			var message = new TcpMessage(payload);
			_sender.Send(message);
		}

		public void Dispose()
		{
			_socket.Dispose();
		}
	}
}
