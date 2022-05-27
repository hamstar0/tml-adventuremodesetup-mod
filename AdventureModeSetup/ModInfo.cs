using System;
using Terraria;


namespace AdventureModeSetup {
	public struct ModInfo {
		public enum ModTypeFlags {
			GameMode = 1,
			GameRules = 2,
			Info = 4,
			Mechanics = 8,
			Content = 16,
			ModResource = 32,
			Optional = 64,
		}



		////////////////

		public static readonly ModInfo[] AdventureModeMods = new ModInfo[] {
			new ModInfo(
				"AdventureMode",
				"Adventure Mode - Basics",
				new Version( 0, 57, 0 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanics
					| ModTypeFlags.Content
			),
			new ModInfo(
				"AdventureModeLore",
				"Adventure Mode - Setting",
				new Version( 0, 21, 2, 1 ),
				ModTypeFlags.Info | ModTypeFlags.Content
			),
			new ModInfo(
				"BossChecklist",
				"Boss Checklist",
				new Version( 1, 1, 6, 1 ),
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"BossReigns",
				"Boss Reigns",
				new Version( 1, 4, 0, 4 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics
			),
			new ModInfo(
				"Bullwhip",
				"Bullwhip",
				new Version( 1, 6, 3, 1 ),
				ModTypeFlags.Content | ModTypeFlags.Optional
			),
			new ModInfo(
				"CursedBones",
				"Cursed Bones",
				new Version( 1, 1, 1, 3 ),
				ModTypeFlags.Content
			),
			new ModInfo(
				"CursedBrambles",
				"Cursed Brambles",
				new Version( 2, 0, 0 ),
				ModTypeFlags.Content | ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Enraged",
				"Enraged!",
				new Version( 1, 0, 0 ),
				ModTypeFlags.Mechanics
			),
			new ModInfo(
				"Ergophobia",
				"Ergophobia",
				new Version( 1, 15, 0 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Content | ModTypeFlags.Mechanics
			),
			new ModInfo(
				"FindableManaCrystals",
				"Findable Mana Crystals",
				new Version( 2, 2, 0 ),
				ModTypeFlags.Content | ModTypeFlags.Mechanics
			),
			new ModInfo(
				"Grappletech",
				"Grappletech",
				new Version( 2, 2, 2, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics
			),
			new ModInfo(
				"GreenHell",
				"Green Hell",
				new Version( 0, 7, 5 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.Content
			),
			new ModInfo(
				"HUDElementsLib",
				"HUD Elements Lib",
				new Version( 5, 0, 0, 4 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"LockedAbilities",
				"Locked Abilities",
				new Version( 1, 3, 1, 2 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"LostExpeditions",
				"Lost Expeditions",
				new Version( 1, 1, 3 ),
				ModTypeFlags.Mechanics | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Messages",
				"Messages",
				new Version( 1, 4, 0, 3 ),
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsCamera",
				"Mod Libs - Camera",
				new Version( 1, 0, 2 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsCore",
				"Mod Libs - Core",
				new Version( 1, 5, 4, 2 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsEntityGroups",
				"Mod Libs - Entity Groups",
				new Version( 1, 1, 1 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsGeneral",
				"Mod Libs - General",
				new Version( 1, 7, 3 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsInterMod",
				"Mod Libs - Inter-Mod",
				new Version( 1, 2, 1 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsMaps",
				"Mod Libs - Maps",
				new Version( 2, 0, 1 ),
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsNet",
				"Mod Libs - Networking",
				new Version( 1, 0, 3 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsNPCDialogue",
				"Mod Libs - NPC Dialogue",
				new Version( 1, 0, 1 ),
				ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsTiles",
				"Mod Libs - Tiles",
				new Version( 1, 1, 1 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsUI",
				"Mod Libs - UI",
				new Version( 1, 2, 2 ),
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsUtilityContent",
				"Mod Libs - Utility Content",
				new Version( 1, 1, 1 ),
				ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModUtilityPanels",
				"Mod Utility Panels",
				new Version( 2, 1, 1, 2 ),
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"MoreItemInfo",
				"More Item Info",
				new Version( 1, 5, 0 ),
				ModTypeFlags.Info | ModTypeFlags.Optional
			),
			new ModInfo(
				"MountedMagicMirrors",
				"Mounted Magic Mirrors",
				new Version( 1, 2, 1, 1 ),
				ModTypeFlags.Mechanics | ModTypeFlags.Content
			),
			new ModInfo(
				"Necrotis",
				"Necrotis",
				new Version( 2, 2, 6, 4 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"Nihilism",
				"Nihilism",
				new Version( 4, 0, 0 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Objectives",
				"Objectives",
				new Version( 0, 9, 0, 2 ),
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Orbs",
				"Orbs",
				new Version( 1, 9, 0, 4 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PKEMeter",
				"P.K.E Analysis Device",
				new Version( 4, 1, 0 ),
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanics | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PotLuck",
				"Pot Luck",
				new Version( 2, 1, 0 ),
				ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"PowerfulMagic",
				"Powerful Magic",
				new Version( 1, 11, 1 ),
				ModTypeFlags.Mechanics | ModTypeFlags.ModResource
			),
			new ModInfo(
				"QuickRope",
				"Quick Rope",
				new Version( 0, 3, 0 ),
				ModTypeFlags.Mechanics | ModTypeFlags.Optional
			),
			new ModInfo(
				"ReadableBooks",
				"Readable Books",
				new Version( 1, 3, 0, 1 ),
				ModTypeFlags.Info | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"RuinedItems",
				"Ruined Items",
				new Version( 2, 11, 3 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanics
			),
			new ModInfo(
				"SoulBarriers",
				"Soul Barriers",
				new Version( 3, 3, 1 ),
				ModTypeFlags.Mechanics | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			//new ModInfo(
			//	"SpiritWalking",
			//	"Spirit Walking",
			//	new Version( 1, 1, 2, 1 ),
			//	ModTypeFlags.Mechanic | ModTypeFlags.Content
			//),
			new ModInfo(
				"SteampunkArsenal",
				"Steampunk Arsenal",
				new Version( 0, 18, 0, 4 ),
				ModTypeFlags.Content | ModTypeFlags.Optional
			),
			new ModInfo(
				"Surroundings",
				"Surroundings",
				new Version( 1, 2, 0, 4 ),
				ModTypeFlags.Content | ModTypeFlags.Optional
			),
			new ModInfo(
				"TerrainRemixer",
				"Terrain Remixer",
				new Version( 0, 13, 0 ),
				ModTypeFlags.GameRules | ModTypeFlags.ModResource
			),
			new ModInfo(
				"TheMadRanger",
				"The Mad Ranger",
				new Version( 1, 11, 0, 2 ),
				ModTypeFlags.Content | ModTypeFlags.Optional
			),
			new ModInfo(
				"TheTrickster",
				"The Trickster",
				new Version( 1, 7, 5, 9 ),
				ModTypeFlags.GameRules | ModTypeFlags.Content
			),
			new ModInfo(
				"TrashHistory",
				"Trash History",
				new Version( 1, 1, 0, 1 ),
				ModTypeFlags.Mechanics | ModTypeFlags.Optional
			),
			new ModInfo(
				"WorldGates",
				"World Gates",
				new Version( 1, 3, 1, 1 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanics | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			)
		};



		////////////////
		
		public string Name;
		public string DisplayName;
		public Version MinVersion;
		public ModTypeFlags InfoFlags;



		////////////////
		
		public ModInfo( string name, string displayName, Version minVersion, ModTypeFlags infoFlags ) {
			this.Name = name;
			this.DisplayName = displayName;
			this.MinVersion = minVersion;
			this.InfoFlags = infoFlags;
		}
	}
}