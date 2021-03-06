using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private IEnumerable<(UIElement elem, float height)> CreateWelcomeElements(
					ISet<ModInfo> outdatedMods,
					ISet<ModInfo> missingMods,
					ISet<ModInfo> deactivatedMods,
					ISet<string> extraMods ) {
			UIPanel listPanel;
			var elemList = new List<(UIElement, float)>();

			//

			float listHeight = 96f;

			//

			var mymod = AMSMod.Instance;

			string version = $" v{Math.Max( mymod.Version.Major, 1 )}";

			if( mymod.Version.Major < 0 || mymod.Version.Build != 0 ) {
				version += $" (beta)";
			}

			elemList.Add( (new UIText($"Welcome to Adventure Mode{version}", 0.75f, true), 40f) );

			//

			if( outdatedMods.Count > 0 ) {
				AMSMod.Instance.Logger.Info(
					"Outdated mods detected: "
					+string.Join( ", ", outdatedMods.Select(mi=>mi.Name) )
				);
				
				elemList.Add( (new UIText($"Detected {outdatedMods.Count} outdated mods."), 24f) );
			}

			if( missingMods.Count > 0 ) {
				elemList.Add( (new UIText("Tthe following mods will need to be installed:"), 20f) );

				listPanel = this.CreateMissingModsListPanel(
					missingMods: missingMods,
					listHeight: listHeight,
					missingModsListElement: out this.MissingModsListElement
				);
				elemList.Add( (listPanel, listHeight + 4f) );
			}

			//

			if( extraMods.Count > 0 ) {
				elemList.Add( (new UIText("The following mods will be disabled (after backup):"), 20f) );

				listPanel = this.CreateExtraModsListPanel(
					extraMods: extraMods,
					listHeight: listHeight,
					listElement: out this.ExtraModsListElement
				);
				elemList.Add( (listPanel, listHeight + 4f) );
			}

			//

			return elemList;
		}
	}
}