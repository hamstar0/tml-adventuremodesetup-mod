using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private void DeployInstallPromptMenuMode_If( ISet<string> missingMods, ISet<string> unloadedMods ) {
			if( unloadedMods.Count() == 0 ) {
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

			//
			
			Main.PlaySound( SoundID.MenuOpen, -1, -1, 1, 1f, 0f );
		}
	}
}