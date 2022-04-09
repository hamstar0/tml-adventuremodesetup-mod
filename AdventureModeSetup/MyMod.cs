using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public static AMSMod Instance => ModContent.GetInstance<AMSMod>();



		////////////////

		private UIInstallPromptDialog InstallPromptUI;

		////

		private ISet<ModInfo> OutdatedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> UnloadedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> MissingMods = new HashSet<ModInfo>();
		private ISet<string> ExtraMods = new HashSet<string>();

		////
		
		private bool HasPostAddRecipes = false;
		private bool HasPostSetupContent = false;
		private bool HasAddRecipeGroups = false;



		////////////////

		public override void Load() {
			if( Main.dedServ ) {
				return;
			}

			//

			this.InstallPromptUI = new UIInstallPromptDialog();

			//

			this.GetEachModStatus(
				gameModeModEntries: ModInfo.NeededMods,
				outdatedMods: out this.OutdatedMods,
				unloadedMods: out this.UnloadedMods,
				nonexistentMods: out this.MissingMods,
				extraMods: out this.ExtraMods
			);

			//
			
			On.Terraria.Main.DrawMenu += this.Main_DrawMenu_Inject;

			//

			this.HasAddRecipeGroups = false;
			this.HasPostAddRecipes = false;
			this.HasPostSetupContent = false;
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

		 private bool _HasPrompted = false;

		private void Main_DrawMenu_Inject(
					On.Terraria.Main.orig_DrawMenu orig,
					Main self,
					GameTime gameTime ) {
			orig.Invoke( self, gameTime );

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this._HasPrompted ) {
					this._HasPrompted = true;

					this.OpenInstallPromptMenu_If(
						outdatedMods: this.OutdatedMods,
						missingMods: this.MissingMods,
						unloadedMods: this.UnloadedMods,
						extraMods: this.ExtraMods
					);
				}
			}
		}
	}
}