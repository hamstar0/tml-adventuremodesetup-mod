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



		internal void OpenInstallPromptMenu_If(
					ISet<ModInfo> outdatedMods,
					ISet<ModInfo> missingMods,
					ISet<ModInfo> deactivatedMods,
					ISet<string> extraMods ) {
			bool properGameModeLoadout = outdatedMods.Count == 0
				&& missingMods.Count == 0
				&& deactivatedMods.Count == 0
				&& extraMods.Count == 0;

			if( !AMSConfig.Instance.ForceInstallPromptEachLoad ) {
				if( properGameModeLoadout ) {
this.Logger.Warn( "INSTALLER OPEN FAIL 1 " );
					return;
				}
			}

			if( Main.netMode != NetmodeID.SinglePlayer || Main.dedServ ) {
this.Logger.Warn( "INSTALLER OPEN FAIL 2 " );
				return;
			}

			//
			
			Main.MenuUI.SetState( this.InstallPromptUI );

			this.InstallPromptUI.OnInitializeFinal(
				outdatedMods: outdatedMods,
				missingMods: missingMods,
				deactivatedMods: deactivatedMods,
				extraMods: extraMods
			);

			//

			//Main.menuMode = Math.Abs( Math.Abs(this.GetHashCode()) + 20000 );
			Main.menuMode = 888;
		}
	}
}