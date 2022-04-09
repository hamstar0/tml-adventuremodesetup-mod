using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private UIPanel CreateExtraModsListPanel(
					ISet<string> extraMods,
					float listHeight,
					out UIList listElement ) {
			var listElemContainer = new UIPanel();
			listElemContainer.Width.Set( 0f, 1f );
			listElemContainer.Height.Set( listHeight, 0f );

			{
				listElement = new UIList();
				listElement.Top.Set( 0f, 0f );
				listElement.Width.Set( -25f, 1f );
				listElement.Height.Set( 0f, 1f );

				listElemContainer.Append( listElement );

				//

				var scrollbar = new UIScrollbar();
				scrollbar.Top.Set( 0f, 0f );
				scrollbar.Left.Set( -24f, 1f );
				scrollbar.Height.Set( -8f, 1f );
				scrollbar.SetView( 100f, 1000f );
				scrollbar.HAlign = 0f;

				listElement.SetScrollbar( scrollbar );

				listElemContainer.Append( scrollbar );

				//

				IEnumerable<UIElement> listItemElems = extraMods
					.OrderBy( mn => ModLoader.GetMod(mn).DisplayName )
					.Select( mn => ModLoader.GetMod(mn) )
					.Select( m => new UIText(m.DisplayName) );
				foreach( UIElement listElem in listItemElems ) {
					listElement.Add( listElem );
				}
			}

			return listElemContainer;
		}
	}
}