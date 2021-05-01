namespace Messages.Models
{
	public readonly struct Classification
	{
		public Class Class { get; }
		public Realm Realm { get; }
		public Race Race => (Race)(_etc & 0x0F);
		public Gender Gender => (Gender)(_etc & 0x80);

		private readonly byte _etc;

		public Classification(Class klass, Realm realm, Race race, Gender gender)
		{
			Class = klass;
			Realm = realm;
			_etc = (byte)((byte)gender + (byte)race);
		}
	}
}
