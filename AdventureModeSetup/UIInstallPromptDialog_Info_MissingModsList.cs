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
					float listHeight,
					out UIList missingModsListElement ) {
			var missingModListContainer = new UIPanel();
			missingModListContainer.Width.Set( 0f, 1f );
			missingModListContainer.Height.Set( listHeight, 0f );

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
	}
}