using System;

namespace Messages
{
	public static class MessageType
	{
		public static class ClientToServer
		{
			private static readonly string[] _names = GetNames(typeof(ClientToServer));

			public static string GetName(byte value, int protocolVersion)
			{
				return _names[value];
			}

			public const byte HouseMenuRequest = 0x00;
			public const byte HouseEdit = 0x01;
			public const byte HouseUserPermissionRequest = 0x03;
			public const byte HousePermissionRequest = 0x05;
			public const byte HouseUserPermissionSet = 0x06;
			public const byte HousePermissionSet = 0x07;
			public const byte HouseEnterLeave = 0x0B;
			public const byte HousePlaceItem = 0x0C;
			public const byte PlayerPickupHouseItem = 0x0D;
			public const byte HouseDecorationRotate = 0x0E;
			public const byte CharacterSelectRequest = 0x10;
			public const byte MarketSearchRequest = 0x11;
			public const byte UDPInitRequest = 0x14;
			public const byte HouseDecorationRequest = 0x18;
			public const byte SetMarketPrice = 0x1A;
			public const byte WithDrawMerchantMoney = 0x1C;
			public const byte ClientCrash = 0x37;
			public const byte QuestRewardChosen = 0x40;
			public const byte ShowWarmapRequest = 0x48;
			public const byte MinotaurRelicWindow = 0x4C;
			public const byte RemoveQuestRequest = 0x4F;
			public const byte TrainHandler = 0x53;
			public const byte BuyHookPoint = 0x64;
			public const byte WarmapBonusRequest = 0x66;
			public const byte KeepComponentInteract = 0x6F;
			public const byte UseSlot = 0x71;
			public const byte PlayerAttackRequest = 0x74;
			public const byte RemoveConcentrationEffect = 0x76;
			public const byte BuyRequest = 0x78;
			public const byte SellRequest = 0x79;
			public const byte ObjectInteractRequest = 0x7A;
			public const byte TrainWindowHandler = 0x7B;
			public const byte TrainRequest = 0x7C;
			public const byte UseSpell = 0x7D;
			public const byte DestroyItemRequest = 0x80;
			public const byte DialogResponse = 0x82;
			public const byte LookingForGroupFlag = 0x84;
			public const byte LookingForGroup = 0x85;
			public const byte InviteToGroup = 0x87;
			public const byte PetWindow = 0x8A;
			public const byte PlayerRegionChangeRequest = 0x90;
			public const byte DoorRequest = 0x99;
			public const byte RegionListRequest = 0x9D;
			public const byte DisbandFromGroup = 0x9F;
			public const byte PingRequest = 0xA3;
			public const byte ObjectUpdateRequest = 0xA5;
			public const byte LoginRequest = 0xA7;
			public const byte PositionUpdate = 0xA9;
			public const byte SlashCommand = 0xAF;
			public const byte PlayerTarget = 0xB0;
			public const byte PickUpRequest = 0xB5;
			public const byte PlayerHeadingUpdate = 0xBA;
			public const byte UseSkill = 0xBB;
			public const byte CreateNPCRequest = 0xBE;
			public const byte GameOpenRequest = 0xBF;
			public const byte CharacterDeleteRequest = 0xC0;
			public const byte PlayerSitRequest = 0xC7;
			public const byte PlayerDismountRequest = 0xC8;
			public const byte BonusesListRequest = 0xCA;
			public const byte NameCheck = 0xCB;
			public const byte CheckLOSRequest = 0xD0;
			public const byte WorldInitRequest = 0xD4;
			public const byte CreatePlayerRequest = 0xD5;
			public const byte DetailRequest = 0xD8;
			public const byte PlayerMoveItem = 0xDD;
			public const byte PlayerAppraiseItemRequest = 0xE0;
			public const byte EmblemDialogResponse = 0xE2;
			public const byte ShipHookPoint = 0xE4;
			public const byte PlayerInitRequest = 0xE8;
			public const byte ModifyTrade = 0xEB;
			public const byte PlayerGroundTarget = 0xEC;
			public const byte CraftRequest = 0xED;
			public const byte UDPPingRequest = 0xF2;
			public const byte Handshake = 0xF4;
			public const byte SiegeCommandRequest = 0xF5;
			public const byte PlayerCancelsEffect = 0xF8;
			public const byte CharacterOverviewRequest = 0xFC;
			public const byte CharacterCreateRequest = 0xFF;
		}

		public static class ServerToClient
		{
			private static readonly string[] _names = GetNames(typeof(ServerToClient));

			public static string GetName(byte value, int protocolVersion)
			{
				return _names[value];
			}

			public const byte InventoryUpdate = 0x02;
			public const byte HouseUserPermissions = 0x03;
			public const byte CharacterJump = 0x04;
			public const byte HousingPermissions = 0x05;
			public const byte HouseEnter = 0x08;
			public const byte HousingItem = 0x09;
			public const byte HouseExit = 0x0A;
			public const byte HouseTogglePoints = 0x0F;
			public const byte MovingObjectCreate = 0x12;
			public const byte EquipmentUpdate = 0x15;
			public const byte VariousUpdate = 0x16;
			public const byte MerchantWindow = 0x17;
			public const byte HouseDecorationRotate = 0x18;
			public const byte SpellEffectAnimation = 0x1B;
			public const byte ConsignmentMerchantMoney = 0x1E;
			public const byte MarketExplorerWindow = 0x1F;
			public const byte PositionAndObjectID = 0x20;
			public const byte DebugMode = 0x21;
			public const byte HandshakeResponse = 0x22;
			public const byte SessionId = 0x28;
			public const byte PingReply = 0x29;
			public const byte LoginGranted = 0x2A;
			public const byte CharacterInitFinished = 0x2B;
			public const byte LoginDenied = 0x2C;
			public const byte GameOpenResponse = 0x2D;
			public const byte UDPInitReply = 0x2F;
			public const byte MinotaurRelicMapRemove = 0x45;
			public const byte MinotaurRelicMapUpdate = 0x46;
			public const byte WarMapClaimedKeeps = 0x49;
			public const byte WarMapDetailUpdate = 0x4A;
			public const byte PlayerCreate172 = 0x4B;
			public const byte VisualEffect = 0x4C;
			public const byte ControlledHorse = 0x4E;
			public const byte MinotaurRelicRealm = 0x59;
			public const byte XFire = 0x5C;
			public const byte KeepComponentInteractResponse = 0x61;
			public const byte KeepClaim = 0x62;
			public const byte KeepComponentHookpointStore = 0x63;
			public const byte KeepComponentHookpointUpdate = 0x65;
			public const byte WarmapBonuses = 0x66;
			public const byte KeepComponentUpdate = 0x67;
			public const byte KeepInfo = 0x69;
			public const byte KeepRealmUpdate = 0x6A;
			public const byte KeepRemove = 0x6B;
			public const byte KeepComponentInfo = 0x6C;
			public const byte KeepComponentDetailUpdate = 0x6D;
			public const byte KeepComponentRemove = 0x6E;
			public const byte GroupMemberUpdate = 0x70;
			public const byte SpellCastAnimation = 0x72;
			public const byte InterruptSpellCast = 0x73;
			public const byte AttackMode = 0x74;
			public const byte ConcentrationList = 0x75;
			public const byte TrainerWindow = 0x7B;
			public const byte Time = 0x7E;
			public const byte UpdateIcons = 0x7F;
			public const byte Dialog = 0x81;
			public const byte QuestEntry = 0x83;
			public const byte FindGroupUpdate = 0x86;
			public const byte PetWindow = 0x88;
			public const byte PlayerRevive = 0x89;
			public const byte PlayerModelTypeChange = 0x8D;
			public const byte CharacterPoints = 0x91;
			public const byte Weather = 0x92;
			public const byte DoorState = 0x99;
			public const byte RegionList = 0x9E;
			public const byte CharacterRegion = 0xB1;
			public const byte ObjectUpdate = 0xA1;
			public const byte RemoveObject = 0xA2;
			public const byte Quit = 0xA4;
			public const byte PlayerPosition = 0xA9;
			public const byte CharacterStatus = 0xAD;
			public const byte PlayerDeath = 0xAE;
			public const byte Message = 0xAF;
			public const byte CharacterSpeed = 0xB6;
			public const byte RegionChanged = 0xB7;
			public const byte PlayerHeading = 0xBA;
			public const byte CombatAnimation = 0xBC;
			public const byte Encumberance = 0xBD;
			public const byte BadNameCheckReply = 0xC3;
			public const byte DetailWindow = 0xC4;
			public const byte AddFriend = 0xC5;
			public const byte RemoveFriend = 0xC6;
			public const byte Riding = 0xC8;
			public const byte SoundEffect = 0xC9;
			public const byte NameCheckResponse = 0xCC;
			public const byte HouseCreate = 0xD1;
			public const byte HouseChangeGarden = 0xD2;
			public const byte PlaySound = 0xD3;
			public const byte PlayerCreate = 0xD4;
			public const byte DisableSkills = 0xD6;
			public const byte DelveInfo = 0xD8;
			public const byte ObjectCreate = 0xD9;
			public const byte NPCCreate = 0xDA;
			public const byte ModelChange = 0xDB;
			public const byte ObjectGuildID = 0xDE;
			public const byte ChangeGroundTarget = 0xDF;
			public const byte ObjectDelete = 0xE1;
			public const byte EmblemDialogue = 0xE2;
			public const byte SiegeWeaponAnimation = 0xE3;
			public const byte TradeWindow = 0xEA;
			public const byte ObjectDataUpdate = 0xEE;
			public const byte RegionSound = 0xEF;
			public const byte CharacterCreateReply = 0xF0;
			public const byte TimerWindow = 0xF3;
			public const byte SiegeWeaponInterface = 0xF5;
			public const byte ChangeTarget = 0xF6;
			public const byte HelpWindow = 0xF7;
			public const byte EmoteAnimation = 0xF9;
			public const byte MoneyUpdate = 0xFA;
			public const byte StatsUpdate = 0xFB;
			public const byte CharacterOverview = 0xFD;
			public const byte PlayerRealm = 0xFE;
			public const byte MasterLevelWindow = 0x13;
			public const byte CheckLOSRequest = 0xD0;
		}

		private static string[] GetNames(Type type)
		{
			// this will get more complicated if message types change in other client versions
			var names = new string[256];
			foreach(var field in type.GetFields())
			{
				names[(byte)field.GetValue(null)] = field.Name;
			}
			return names;
		}
	}
}
