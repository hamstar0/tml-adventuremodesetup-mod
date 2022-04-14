using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		 private bool _HasPrompted = false;



		////////////////

		private void Main_DrawMenu_Inject(
					On.Terraria.Main.orig_DrawMenu orig,
					Main self,
					GameTime gameTime ) {
			this.DrawFullLogo_If( Main.spriteBatch );

			//
			
			orig.Invoke( self, gameTime );

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this._HasPrompted ) {
					if( Main.menuMode == 888 && Main.MenuUI.CurrentState == this.InstallPromptUI ) {
						this._HasPrompted = true;
					}

					if( !this._HasPrompted ) {
						this.OpenInstallPromptMenu_If(
							outdatedMods: this.OutdatedMods,
							missingMods: this.MissingMods,
							deactivatedMods: this.DeactivatedMods,
							extraMods: this.ExtraMods
						);
					}
				}
			}
		}
	}
}