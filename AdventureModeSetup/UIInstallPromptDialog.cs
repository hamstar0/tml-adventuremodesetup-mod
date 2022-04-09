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
		private UIList ExtraModsListElement;



		////////////////

		public UIInstallPromptDialog() : base() {
		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			Main.MenuUI.Update( Main._drawInterfaceGameTime ?? new GameTime() );	// ?!

			//
			
			base.Draw( spriteBatch );

			//

			Texture2D tex = AMSMod.Instance.GetTexture( "logo" );
			var dest = this.DialogPanel.GetDimensions().ToRectangle();

			//

			float scale = (float)dest.Width / (float)tex.Width;

			dest.X += 4;
			dest.Y += 32;
			dest.Width -= 8;
			dest.Height = (int)((float)tex.Height * scale);
//AMSMod.Instance.Logger.Info( "scale: "+scale+ ", dest: "+dest );

			//

			spriteBatch.Draw(
				texture: tex,
				destinationRectangle: dest,
				color: Color.White * 0.15f
			);
		}
	}
}