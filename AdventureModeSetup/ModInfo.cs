using System;
using Terraria;


namespace AdventureModeSetup {
	public struct ModInfo {
		public enum ModTypeFlags {
			GameMode = 1,
			GameRules = 2,
			Info = 4,
			Mechanic = 8,
			Content = 16,
			ModResource = 32,
		}



		////////////////

		public static readonly ModInfo[] NeededMods = new ModInfo[] {
			new ModInfo(
				"AdventureMode",
				"Adventure Mode - Basics",
				new Version( 0, 42, 0 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic
					| ModTypeFlags.Content
			),
			new ModInfo(
				"AdventureModeLore",
				"Adventure Mode - Setting",
				new Version( 0, 20, 2 ),
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
				new Version( 1, 3, 1, 2 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Bullwhip",
				"Bullwhip",
				new Version( 1, 5, 1 ),
				ModTypeFlags.Content
			),
			new ModInfo(
				"CursedBones",
				"Cursed Bones",
				new Version( 1, 0, 0 ),
				ModTypeFlags.Content
			),
			new ModInfo(
				"CursedBrambles",
				"Cursed Brambles",
				new Version( 1, 3, 1, 2 ),
				ModTypeFlags.Content | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Enraged",
				"Enraged!",
				new Version( 0, 13, 1, 1 ),
				ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Ergophobia",
				"Ergophobia",
				new Version( 1, 14, 0, 2 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Content | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"FindableManaCrystals",
				"Findable Mana Crystals",
				new Version( 2, 1, 0 ),
				ModTypeFlags.Content | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Grappletech",
				"Grappletech",
				new Version( 2, 2, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"GreenHell",
				"Green Hell",
				new Version( 0, 7, 4 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
			),
			new ModInfo(
				"HUDElementsLib",
				"HUD Elements Lib",
				new Version( 4, 2, 0 ),
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"LockedAbilities",
				"Locked Abilities",
				new Version( 1, 3, 1, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"LostExpeditions",
				"Lost Expeditions",
				new Version( 1, 1, 0 ),
				ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Messages",
				"Messages",
				new Version( 1, 3, 0 ),
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
				new Version( 1, 5, 3 ),
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
				new Version( 1, 7, 1 ),
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
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
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
				ModTypeFlags.Info
			),
			new ModInfo(
				"MountedMagicMirrors",
				"Mounted Magic Mirrors",
				new Version( 1, 2, 2, 1 ),
				ModTypeFlags.Mechanic | ModTypeFlags.Content
			),
			new ModInfo(
				"Necrotis",
				"Necrotis",
				new Version( 2, 2, 2 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"Nihilism",
				"Nihilism",
				new Version( 4, 0, 0 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Objectives",
				"Objectives",
				new Version( 0, 8, 0, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Orbs",
				"Orbs",
				new Version( 1, 7, 1 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PKEMeter",
				"P.K.E Analysis Device",
				new Version( 2, 0, 0, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PotLuck",
				"Pot Luck",
				new Version( 2, 1, 0 ),
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"PowerfulMagic",
				"Powerful Magic",
				new Version( 1, 11, 0 ),
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"QuickRope",
				"Quick Rope",
				new Version( 0, 3, 0 ),
				ModTypeFlags.Mechanic
			),
			new ModInfo(
				"ReadableBooks",
				"Readable Books",
				new Version( 1, 3, 0 ),
				ModTypeFlags.Info | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"RuinedItems",
				"Ruined Items",
				new Version( 2, 11, 2, 1 ),
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"SoulBarriers",
				"Soul Barriers",
				new Version( 3, 0, 1 ),
				ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
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
				new Version( 0, 18, 0, 2 ),
				ModTypeFlags.Content
			),
			new ModInfo(
				"Surroundings",
				"Surroundings",
				new Version( 1, 2, 0 ),
				ModTypeFlags.Content
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
				new Version( 1, 9, 6 ),
				ModTypeFlags.Content
			),
			new ModInfo(
				"TheTrickster",
				"The Trickster",
				new Version( 1, 7, 5, 6 ),
				ModTypeFlags.GameRules | ModTypeFlags.Content
			),
			new ModInfo(
				"WorldGates",
				"World Gates",
				new Version( 1, 2, 6, 4 ),
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
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