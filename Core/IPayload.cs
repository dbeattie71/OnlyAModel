using System;

namespace Core
{
	public interface IPayload
	{
		MessageType.ServerToClient Type { get; }
		int Length { get; }
		void Marshal(Span<byte> span);
	}
}
