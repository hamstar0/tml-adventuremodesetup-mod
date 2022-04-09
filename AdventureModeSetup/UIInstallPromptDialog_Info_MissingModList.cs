using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private UIPanel CreateMissingModsListPanel(
					ISet<ModInfo> missingMods,
					float height,
					out UIList missingModsListElement ) {
			var missingModListContainer = new UIPanel();
			missingModListContainer.Width.Set( 0f, 1f );
			missingModListContainer.Height.Set( height, 0f );

			{
				missingModsListElement = new UIList();
				missingModsListElement.Top.Set( 0f, 0f );
				missingModsListElement.Width.Set( -25f, 1f );
				missingModsListElement.Height.Set( 0f, 1f );

				missingModListContainer.Append( missingModsListElement );

				//

				var scrollbar = new UIScrollbar();
				scrollbar.Top.Set( 0f, 0f );
				scrollbar.Left.Set( -24f, 1f );
				scrollbar.Height.Set( -8f, 1f );
				scrollbar.SetView( 100f, 1000f );
				scrollbar.HAlign = 0f;

				missingModsListElement.SetScrollbar( scrollbar );

				missingModListContainer.Append( scrollbar );

				//

				IEnumerable<UIElement> missingModListElements = missingMods
					.OrderBy( m => m.DisplayName )
					.Select( m => new UIText(m.DisplayName) );
				foreach( UIElement listElem in missingModListElements ) {
					missingModsListElement.Add( listElem );
				}
			}

			return missingModListContainer;
		}
		/*private UIPanel CreateMissingModsListPanel( ISet<ModInfo> missingMods, out UIList missingModsListElement ) {
			UIElement RenderMod( ModInfo modInfo ) {
				var elem = new UIText( modInfo.DisplayName );
				return elem;
			}

			//

			float currentY = 0f;

			//

			string welcomeMsg = "Welcome to Adventure Mode!";

			currentY += 32f;

			//

			if( missingMods.Count > 0 ) {
				welcomeMsg += " To begin, the following mods will be installed:";

				//

				this.DialogPanel.Append( this.InitailizeMissingModsList( missingMods ) );
				var missingModListContainer = new UIPanel();
				missingModListContainer.Top.Set( currentY, 0f );
				missingModListContainer.Width.Set( 0f, 1f );
				missingModListContainer.Height.Set( 160f, 0f );

				this.DialogPanel.Append( missingModListContainer );

				{
					var missingModListElem = new UIList();
					missingModListElem.Top.Set( 0f, 0f );
					missingModListElem.Width.Set( -25f, 1f );
					missingModListElem.Height.Set( 0f, 1f );

					missingModListContainer.Append( missingModListElem );

					//

					var scrollbar = new UIScrollbar();
					scrollbar.Top.Set( 0f, 0f );
					scrollbar.Left.Set( -24f, 1f );
					scrollbar.Height.Set( -8f, 1f );
					scrollbar.SetView( 100f, 1000f );
					scrollbar.HAlign = 0f;

					missingModListElem.SetScrollbar( scrollbar );

					missingModListContainer.Append( scrollbar );

					//

					IEnumerable<UIElement> missingModListElements = missingMods
						.OrderBy( m => m.DisplayName )
						.Select( m => RenderMod( m ) );
					foreach( UIElement listElem in missingModListElements ) {
						missingModListElem.Add( listElem );
					}
				}

				currentY += 168f;
			}

			//

			var welcomeText = new UIText( welcomeMsg );
			welcomeText.Top.Set( 0f, 0f );
			this.DialogPanel.Append( welcomeText );

			//

			//int activeMods = ModInfo.NeededMods.Length - unloadedMods.Count;
			string text = $"{unloadedMods.Count} mods will need to be enabled to play this game mode. Your"
				+ " existing enabled mods will be backed up as the 'Pre AM Backup' mod pack (see the Mods->Mod"
				+ " Packs menu)."
				+ " \n "
				+ "After installation, a list of available mod updates will appear. If any mods need updates,"
				+ " download them then, reload your mods (via. Mods menu), and you're ready to play. Happy trails!";

			//

			float containerWidth = this.DialogPanel.GetDimensions().Width;
			containerWidth = containerWidth < 240f
				? 240f
				: containerWidth;

			IList<string> lines = UIInstallPromptDialog.GetFittedLines( text, containerWidth - 16f );

			//

			foreach( string line in lines ) {
				if( line != "" ) {
					var gameModeInfoText = new UIText( line );
					gameModeInfoText.Top.Set( currentY, 0f );
					this.DialogPanel.Append( gameModeInfoText );
				}

				currentY += 24f;
			}
		}*/
	}
}