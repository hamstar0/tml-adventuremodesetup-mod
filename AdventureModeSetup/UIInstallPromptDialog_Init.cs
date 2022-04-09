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

		public void InitializeFinal( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			float currentY = 0f;

			void Append( UIElement elem, float height ) {
				if( elem != null ) {
					elem.Top.Set( currentY, 0f );
					this.Append( elem );
				}

				currentY += height;
			}

			//

			string welcomeMsg = "Welcome to Adventure Mode!";
			if( missingMods.Count > 0 ) {
				welcomeMsg += " To begin, the following mods will be installed:";
			}

			UIPanel listPanel = missingMods.Count > 0
				? this.CreateMissingModsListPanel( missingMods, out this.MissingModsListElement )
				: null;

			IEnumerable<UIElement> infoElems = this.CreateInfoElements( missingMods, unloadedMods );

			//

			Append( new UIText(welcomeMsg), 32f );

			if( listPanel != null ) {
				Append( listPanel, listPanel.GetDimensions().Height );
			}

			foreach( UIElement infoElem in infoElems ) {
				Append( infoElem, 24f );
			}
		}
	}
}