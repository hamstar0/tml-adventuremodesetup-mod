using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private void DeployInstallPromptMenuMode_If( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			if( missingMods.Count == 0 && unloadedMods.Count == 0 ) {
				return;
			}
			if( Main.dedServ ) {
				return;
			}

			//
			
			Main.MenuUI.SetState( this.InstallPromptUI );

			this.InstallPromptUI.UpdateMissingModsDisplayList( missingMods, unloadedMods );

			//

			//Main.menuMode = Math.Abs( Math.Abs(this.GetHashCode()) + 20000 );
			Main.menuMode = 888;
		}
	}
}