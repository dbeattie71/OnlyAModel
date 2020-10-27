namespace Messages.Models
{
	/// <summary>
	/// PvP/PvE modes. Controls things like name colors over creatures' heads
	/// and whether the player can back out of the character select screen
	/// after selecting a realm.
	/// </summary>
	public enum PvPMode : byte
	{
		/// <summary>
		/// Standard RvR. Same realm PCs friendly, other realm PCs enemy.
		/// </summary>
		RvR = 0x00,
		/// <summary>
		/// Standard PvP. All PCs enemy, NPC colors based on level.
		/// </summary>
		PvP = 0x01,
		/// <summary>
		/// Same realm PCs and NPCs friendly, other realm PCs and NPCs enemy.
		/// </summary>
		PvERvR = 0x02,
		/// <summary>
		/// Standard PvE. All PCs friendly, realm NPCs friendly, realmless NPCs enemy.
		/// </summary>
		PvE = 0x03,
		/// <summary>
		/// All PCs friendly, all NPCs enemy.
		/// </summary>
		PvW = 0x04
	}
}
