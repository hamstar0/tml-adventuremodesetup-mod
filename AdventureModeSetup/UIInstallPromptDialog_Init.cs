using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using AdventureModeSetup.Libraries.Classes.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		public override void OnInitialize() {
			this.RemoveAllChildren();

			//
			
			this.DialogPanel = this.CreateDialog();

			//

			var infoLink = new UIWebUrlBasic(
				label: "Homepage",
				url: "https://forums.terraria.org/index.php?threads/adventure-mode.85140/",
				hoverUrl: true,
				scale:	1.25f
			);
			infoLink.Left.Set( -104f, 1f );
			infoLink.Top.Set( 4f, 0f );
			this.DialogPanel.Append( infoLink );
		}


		////////////////

		public void OnInitializeFinal(
					ISet<ModInfo> outdatedMods,
					ISet<ModInfo> missingMods,
					ISet<ModInfo> deactivatedMods,
					ISet<string> extraMods ) {
			float currentTopPos = 0f;

			void Append( UIElement elem, float height ) {
				if( elem != null ) {
					elem.Top.Set( currentTopPos, 0f );
					this.DialogPanel.Append( elem );
				}

				currentTopPos += height;
			}

			//

			IEnumerable<(UIElement elem, float height)> welcomeElems = this.CreateWelcomeElements(
				outdatedMods: outdatedMods,
				missingMods: missingMods,
				deactivatedMods: deactivatedMods,
				extraMods: extraMods
			);

			//

			IEnumerable<(UIElement elem, float height)> infoElems = this.CreateInfoElements(
				outdatedMods: outdatedMods,
				missingMods: missingMods,
				deactivatedMods: deactivatedMods,
				extraMods: extraMods
			);

			//

			foreach( (UIElement welcomeElem, float height) in welcomeElems ) {
				Append( welcomeElem, height );
			}

			foreach( (UIElement infoElem, float height) in infoElems ) {
				Append( infoElem, height );
			}
		}
	}
}