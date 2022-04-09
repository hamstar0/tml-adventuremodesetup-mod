using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		public override void OnInitialize() {
			this.RemoveAllChildren();

			//

			this.DialogPanel = this.CreateDialog();
		}

		////////////////

		public void OnInitializeFinal( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			float currentTopPos = 0f;

			void Append( UIElement elem, float height ) {
				if( elem != null ) {
					elem.Top.Set( currentTopPos, 0f );
					this.DialogPanel.Append( elem );
				}

				currentTopPos += height;
			}

			//

			string welcomeMsg = "Welcome to Adventure Mode!";
			if( missingMods.Count > 0 ) {
				welcomeMsg += " To begin, the following mods will be installed:";
			}

			//

			float listHeight = 96f;

			UIPanel listPanel = missingMods.Count > 0
				? this.CreateMissingModsListPanel( missingMods, listHeight, out this.MissingModsListElement )
				: null;

			//

			IEnumerable<(UIElement elem, float height)> infoElems = this.CreateInfoElements(
				missingMods,
				unloadedMods
			);

			//

			Append( new UIText(welcomeMsg), 32f );

			if( listPanel != null ) {
				Append( listPanel, listHeight );
				currentTopPos += 8f;
			}

			foreach( (UIElement infoElem, float height) in infoElems ) {
				Append( infoElem, height );
			}
		}
	}
}