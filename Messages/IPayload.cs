using Core;

namespace Messages
{
	public interface IPayload : IMarshallable
	{
		MessageType.ServerToClient Type { get; }
	}
}
