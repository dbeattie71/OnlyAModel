namespace Messages.Models
{
	public static class ClassExtensions
	{
		/// <summary>
		/// Convert a <see cref="Class">Class</see> constant to its in-game
		/// name. This is necessary because multiple realms have classes
		/// named Rogue and Mauler.
		/// </summary>
		public static string DisplayName(this Class klass)
		{
			return klass.ToString().Split('_')[0];
		}
	}
}
