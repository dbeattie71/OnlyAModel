namespace Models.Character
{
	public static class ClassExtensions
	{
		/// <summary>
		/// Convert a <see cref="Class">Class</see> constant to its in-game
		/// name. This became necessary because the three Labyrinth classes
		/// are all called Mauler.
		/// </summary>
		public static string DisplayName(this Class klass)
		{
			return klass.ToString().Split('_')[0];
		}
	}
}
