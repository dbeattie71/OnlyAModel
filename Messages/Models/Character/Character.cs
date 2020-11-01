namespace Messages.Models.Character
{
	/// <summary>
	/// Model for <see cref="Messages.ServerToClient.CharacterOverview">CharacterOverview</see> and various <c>CharacterXxxRequest</c> messages.
	/// </summary>
	public class Character
	{
		public byte Level { get; set; }
		public string Name { get; set; }
		public Stats Stats { get; set; }
		public Classification Classification { get; set; }
		public Customization Customization { get; set; }
		public VisibleEquipment VisibleEquipment { get; set; }
		public byte ActiveMainhand { get; set; } // represent differently?
		public byte ActiveOffhand { get; set; } // represent differently?
		public ushort Model { get; set; } // uncertain
		public ushort Region { get; set; }
		public string LocationDescription { get; set; }
	}
}
