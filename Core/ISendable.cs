using System;

namespace Core
{
	/// <summary>
	/// A message that can be marshalled as an <see cref="IMessage"/>.
	/// </summary>
	public interface ISendable
	{
		/// <summary>
		/// True if this message can be sent as a datagram.
		/// </summary>
		bool Datagram { get => false; }
		byte Type { get; }
		int Length(int protocolVersion);
		void Marshal(Span<byte> span, int protocolVersion);
	}
}
