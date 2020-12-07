using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MultiPlayer.Services
{
	public class AuthService
	{
		private readonly IDictionary<string, string> _users = new ConcurrentDictionary<string, string>();

		public bool Authenticate(string user, string password)
		{
			if (_users.TryGetValue(user, out string value))
			{
				return password == value;
			}
			else
			{
				_users.Add(user, password);
				return true;
			}
		}
	}
}
