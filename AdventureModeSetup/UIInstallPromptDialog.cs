using System;
using System.Collections.Generic;
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

			//

			container.Top.Set( 220f, 0f );
			container.Height.Set( -220f, 1f );
		}


		////////////////

		public void UpdateMissingModsDisplayList( ISet<string> missingMods, ISet<string> unloadedMods ) {
			var missingText = new UIText( "Missing mods:\n "+string.Join("\n ", missingMods) );
			var unloadedText = new UIText( "Unloaded mods:\n "+string.Join("\n ", unloadedMods ) );

			this.DialogPanel.Append( missingText );
		}
	}
}