using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private UIPanel DialogPanel;

		private UIList MissingModsListElement;



		////////////////

		public UIInstallPromptDialog() : base() {
		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			Main.MenuUI.Update( Main._drawInterfaceGameTime ?? new GameTime() );	// ?!

			//

			base.Draw( spriteBatch );
		}
	}
}