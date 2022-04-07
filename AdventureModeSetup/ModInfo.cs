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
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic
					| ModTypeFlags.Content
			),
			new ModInfo(
				"AdventureModeLore",
				"Adventure Mode - Setting",
				ModTypeFlags.Info | ModTypeFlags.Content
			),
			new ModInfo(
				"BossChecklist",
				"Boss Checklist",
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"BossReigns",
				"Boss Reigns",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Bullwhip",
				"Bullwhip",
				ModTypeFlags.Content
			),
			new ModInfo(
				"CursedBones",
				"Cursed Bones",
				ModTypeFlags.Content
			),
			new ModInfo(
				"CursedBrambles",
				"Cursed Brambles",
				ModTypeFlags.Content | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Enraged",
				"Enraged!",
				ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Ergophobia",
				"Ergophobia",
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Content | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"FindableManaCrystals",
				"Findable Mana Crystals",
				ModTypeFlags.Content | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"Grappletech",
				"Grappletech",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"GreenHell",
				"Green Hell",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
			),
			new ModInfo(
				"HUDElementsLib",
				"HUD Elements Lib",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"LockedAbilities",
				"Locked Abilities",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"LostExpeditions",
				"Lost Expeditions",
				ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Messages",
				"Messages",
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsCamera",
				"Mod Libs - Camera",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsCamera",
				"Mod Libs - Camera",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsCore",
				"Mod Libs - Core",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsEntityGroups",
				"Mod Libs - Entity Groups",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsGeneral",
				"Mod Libs - General",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsInterMod",
				"Mod Libs - Inter-Mod",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsMaps",
				"Mod Libs - Maps",
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsNet",
				"Mod Libs - Networking",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsNPCDialogue",
				"Mod Libs - NPC Dialogue",
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsTiles",
				"Mod Libs - Tiles",
				ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsUI",
				"Mod Libs - UI",
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModLibsUtilityContent",
				"Mod Libs - Utility Content",
				ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"ModUtilityPanels",
				"Mod Utility Panels",
				ModTypeFlags.Info | ModTypeFlags.ModResource
			),
			new ModInfo(
				"MoreItemInfo",
				"More Item Info",
				ModTypeFlags.Info
			),
			new ModInfo(
				"MountedMagicMirrors",
				"Mounted Magic Mirrors",
				ModTypeFlags.Mechanic | ModTypeFlags.Content
			),
			new ModInfo(
				"Necrotis",
				"Necrotis",
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"Nihilism",
				"Nihilism",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Objectives",
				"Objectives",
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Orbs",
				"Orbs",
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PKEMeter",
				"P.K.E Analysis Device",
				ModTypeFlags.GameRules | ModTypeFlags.Info | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			),
			new ModInfo(
				"PotLuck",
				"Pot Luck",
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"PowerfulMagic",
				"Powerful Magic",
				ModTypeFlags.Mechanic | ModTypeFlags.ModResource
			),
			new ModInfo(
				"QuickRope",
				"Quick Rope",
				ModTypeFlags.Mechanic
			),
			new ModInfo(
				"ReadableBooks",
				"Readable Books",
				ModTypeFlags.Info | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"RuinedItems",
				"Ruined Items",
				ModTypeFlags.GameRules | ModTypeFlags.Mechanic
			),
			new ModInfo(
				"SteampunkArsenal",
				"Steampunk Arsenal",
				ModTypeFlags.Content
			),
			new ModInfo(
				"SoulBarriers",
				"Soul Barriers",
				ModTypeFlags.Mechanic | ModTypeFlags.Content | ModTypeFlags.ModResource
			),
			new ModInfo(
				"Surroundings",
				"Surroundings",
				ModTypeFlags.Content
			),
			new ModInfo(
				"TerrainRemixer",
				"Terrain Remixer",
				ModTypeFlags.GameRules | ModTypeFlags.ModResource
			),
			new ModInfo(
				"TheMadRanger",
				"The Mad Ranger",
				ModTypeFlags.Content
			),
			new ModInfo(
				"TheTrickster",
				"The Trickster",
				ModTypeFlags.GameRules | ModTypeFlags.Content
			),
			new ModInfo(
				"WorldGates",
				"World Gates",
				ModTypeFlags.GameMode | ModTypeFlags.GameRules | ModTypeFlags.Mechanic | ModTypeFlags.Content
					| ModTypeFlags.ModResource
			)
		};



		////////////////
		
		public string Name;
		public string DisplayName;
		public ModTypeFlags InfoFlags;



		////////////////
		
		public ModInfo( string name, string displayName, ModTypeFlags infoFlags ) {
			this.Name = name;
			this.DisplayName = displayName;
			this.InfoFlags = infoFlags;
		}
	}
}