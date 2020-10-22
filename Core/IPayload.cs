using System;

namespace Core
{
	public interface IPayload
	{
		MessageType.ServerToClient Type { get; }
		ushort Length { get; }
		void Marshal(Span<byte> span);
	}
}
