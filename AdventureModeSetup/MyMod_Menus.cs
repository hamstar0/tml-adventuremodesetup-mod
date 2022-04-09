using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		/*public static void OpenModBrowserMenu() {
			Type interfaceType = ReflectionHelpers.GetMainAssembly()
				.GetType( "Terraria.ModLoader.UI.Interface" );

			int modBrowserMenuMode;
			if( !ReflectionHelpers.Get( interfaceType, null, "modBrowserID", out modBrowserMenuMode ) ) {
				LogHelpers.Warn( "Could not switch to mod browser menu context." );
				return;
			}

			Main.PlaySound( SoundID.MenuTick );
			Main.menuMode = modBrowserMenuMode;

			UIState modBrowserUi;
			if( !ReflectionHelpers.Get( interfaceType, null, "modBrowser", out modBrowserUi ) ) {
				LogHelpers.Warn( "Could not acquire mod browser UI." );
				return;
			}

			Timers.SetTimer( "ModHelpersModDownloadPrompt", 5, true, () => {
				if( MenuContextService.GetCurrentMenuUI()?.GetType().Name != "UIModBrowser" ) {
					return false;
				}

				bool isLoading;
				if( !ReflectionHelpers.Get( modBrowserUi, "loading", out isLoading ) ) {
					return false;
				}

				if( isLoading ) {
					return true;
				}

				ModMenuHelpers.ApplyModBrowserFilter( "", false, new List<string>() );
				return false;
			} );
		}*/



		internal void OpenInstallPromptMenu_If( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			if( missingMods.Count == 0 && unloadedMods.Count == 0 ) {
				return;
			}
			if( Main.dedServ ) {
				return;
			}

			//
			
			Main.MenuUI.SetState( this.InstallPromptUI );

			this.InstallPromptUI.InitializeFinal( missingMods, unloadedMods );

			//

			//Main.menuMode = Math.Abs( Math.Abs(this.GetHashCode()) + 20000 );
			Main.menuMode = 888;
		}


		////////////////

		internal void OpenModUpdatesModBrowserMenu_If() {
			if( Main.dedServ ) {
				return;
			}

			//
			
			Main.menuMode = 0;
		}
	}
}