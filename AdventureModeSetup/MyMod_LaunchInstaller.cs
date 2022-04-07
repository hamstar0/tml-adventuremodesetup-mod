using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private void DeployInstallPromptMenuMode_If( IEnumerable<string> missingMods ) {
			if( missingMods.Count() == 0 ) {
				return;
			}
			if( Main.dedServ ) {
				return;
			}

			//

			Main.MenuUI.SetState( this.InstallPromptUI );

			this.InstallPromptUI.UpdateMissingModsDisplayList( missingMods );

			//

			//Main.menuMode = Math.Abs( Math.Abs(this.GetHashCode()) + 20000 );
			Main.menuMode = 888;

			//
			
			Main.PlaySound( SoundID.MenuOpen, -1, -1, 1, 1f, 0f );
		}
	}
}