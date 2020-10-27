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
		// region might actually be 1 byte
		// followed by 1 byte, normally 0x00
		// but non-zero if player is in a region that client doesn't support
		public ushort Region { get; set; } // uncertain
		public string LocationDescription { get; set; }
	}
}
