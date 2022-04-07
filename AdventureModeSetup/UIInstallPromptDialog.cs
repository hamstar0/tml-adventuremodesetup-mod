using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	class UIInstallPromptDialog : UIState {
		private UIPanel DialogPanel;



		////////////////

		public UIInstallPromptDialog() : base() {
		}


		public override void OnActivate() {
			this.RemoveAllChildren();

			//

			var container = new UIElement();
			container.Top.Set( 220f, 0f );
			container.HAlign = 0.5f;
			container.MaxWidth.Set( 800f + 100f, 0f );
			container.MinWidth.Set( 600f + 100f, 0f );
			container.Width.Set( 0f, 0.8f );
			container.Height.Set( -220f, 1f );
			this.Append( container );

			this.DialogPanel = new UIPanel();
			this.DialogPanel.Width.Set( 0f, 1f );
			this.DialogPanel.Height.Set( 0f, 1f );
			container.Append( this.DialogPanel );
		}


		////////////////

		public void UpdateMissingModsDisplayList( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			UIElement RenderMod( ModInfo modInfo ) {
				var elem = new UIText( modInfo.DisplayName );
				return elem;
			}

			//

			float top = 0f;

			//

			var welcomeText = new UIText( "Welcome to Adventure Mode!" );
			this.DialogPanel.Append( welcomeText );

			top += 24f;

			//

			if( missingMods.Count > 0 ) {
				welcomeText.SetText( welcomeText.Text+" To begin, the following mods will be installed:" );

				//

				var missingModListPanel = new UIList();
				missingModListPanel.Top.Set( top, 0f );
				missingModListPanel.Width.Set( -25f, 1f );
				missingModListPanel.Height.Set( 240f, 0f );
				missingModListPanel.AddRange( missingMods
					.OrderBy( m => m.Name )
					.Select( m => RenderMod(m) )
				);
				this.DialogPanel.Append( missingModListPanel );

				var scrollbar = new UIScrollbar();
				scrollbar.Top.Set( top + 8f, 0f );
				scrollbar.Left.Set( -24f, 1f );
				scrollbar.Height.Set( -16f, 1f );
				scrollbar.SetView( 100f, 1000f );
				scrollbar.HAlign = 0f;

				missingModListPanel.SetScrollbar( scrollbar );

				top += 240f;
			}

			//

			var gameModeInfoText = new UIText( $"{ModInfo.NeededMods.Length} mods will need to be enabled to play"
				+" this game mode." );
			gameModeInfoText.Top.Set( top, 0f );
			this.DialogPanel.Append( gameModeInfoText );
		}
	}
}