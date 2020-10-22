using System;

namespace Messages
{
	/// <summary>
	/// Applicable to public instance methods that receive <c>(Server,
	/// MessageEventArgs, T)</c>, where <c>T</c> is any type returned by an
	/// autowired factory method in the same handler.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class AutowiredHandlerAttribute : Attribute { }
}
