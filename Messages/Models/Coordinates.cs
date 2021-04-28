namespace Messages.Models
{
	public readonly struct Coordinates
	{
		public readonly float X;
		public readonly float Y;
		public readonly float Z;
		public readonly ushort Heading;

		public Coordinates(float x, float y, float z, ushort heading)
		{
			X = x;
			Y = y;
			Z = z;
			Heading = heading;
		}
	}
}
