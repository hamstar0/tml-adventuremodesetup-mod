using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public static AMSMod Instance => ModContent.GetInstance<AMSMod>();


		////////////////

		public static readonly string[] NeededMods = new string[] {
			"AdventureMode",
			"AdventureModeLore",
			"BossChecklist",
			"BossReigns",
			"Bullwhip",
			"CursedBones",
			"CursedBrambles",
			"Enraged",
			"Ergophobia",
			"FindableManaCrystals",
			"Grappletech",
			"GreenHell",
			"HUDElementsLib",
			"LockedAbilities",
			"LostExpeditions",
			"Messages",
			"ModLibsCamera",
			"ModLibsCore",
			"ModLibsEntityGroups",
			"ModLibsGeneral",
			"ModLibsInterMod",
			"ModLibsMaps",
			"ModLibsNet",
			"ModLibsNPCDialogue",
			"ModLibsTiles",
			"ModLibsUI",
			"ModLibsUtilityContent",
			"ModUtilityPanels",
			"MoreItemInfo",
			"MountedMagicMirrors",
			"Necrotis",
			"Nihilism",
			"Objectives",
			"Orbs",
			"PKEMeter",
			"PotLuck",
			"PowerfulMagic",
			"QuickRope",
			"ReadableBooks",
			"RuinedItems",
			"SteampunkArsenal",
			"SoulBarriers",
			"Surroundings",
			"TerrainRemixer",
			"TheMadRanger",
			"TheTrickster",
			"WorldGates"
		};



		////////////////

		private ISet<string> UnloadedMods = new HashSet<string>();
		private ISet<string> MissingMods = new HashSet<string>();

		////
		
		private UIInstallPromptDialog InstallPromptUI;

		////

		private bool HasPostAddRecipes = false;
		private bool HasPostSetupContent = false;
		private bool HasAddRecipeGroups = false;

		////

		private bool HasPrompted = false;



		////////////////

		public override void Load() {
			if( !Main.dedServ ) {
				this.InstallPromptUI = new UIInstallPromptDialog();
			}

			//

			this.GetEachModStatus( AMSMod.NeededMods, out this.UnloadedMods, out this.MissingMods );

			//

			On.Terraria.Main.DrawMenu += this.Main_DrawMenu_Inject;
		}


		////////////////

		public override void PostAddRecipes() {
			this.HasPostAddRecipes = true;
		}

		public override void PostSetupContent() {
			this.HasPostSetupContent = true;
		}

		public override void AddRecipeGroups() {
			this.HasAddRecipeGroups = true;
		}


		////////////////

		private void Main_DrawMenu_Inject(
					On.Terraria.Main.orig_DrawMenu orig,
					Main self,
					GameTime gameTime ) {
			orig.Invoke( self, gameTime );

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this.HasPrompted ) {
					this.HasPrompted = true;

					this.DeployInstallPromptMenuMode_If( this.MissingMods, this.UnloadedMods );
				}
			}
		}
	}
}