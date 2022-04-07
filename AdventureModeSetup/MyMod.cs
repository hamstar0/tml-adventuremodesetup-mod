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

		private ISet<ModInfo> UnloadedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> MissingMods = new HashSet<ModInfo>();

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

			this.GetEachModStatus( ModInfo.NeededMods, out this.UnloadedMods, out this.MissingMods );

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

					this.DeployInstallPromptMenuMode_If( this.MissingMods, this.UnloadedMods );
				}
			}
		}
	}
}