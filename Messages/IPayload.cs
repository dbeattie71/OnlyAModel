using Core;

namespace Messages
{
	public interface IPayload : IMarshallable
	{
		byte MessageType { get; }
	}
}
