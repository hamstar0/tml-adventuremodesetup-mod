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

			Texture2D tex = AMSMod.Instance.Logo.LogoTex;
			if( tex == null ) {
				return;
			}

			//

			var dest = this.DialogPanel.GetDimensions().ToRectangle();

			//

			float scale = (float)dest.Width / (float)tex.Width;
			float texHeight = (float)tex.Height * scale;

			dest.X += 4;
			dest.Y += dest.Height - ((int)texHeight + 64);
			dest.Width -= 8;
			dest.Height = (int)texHeight;
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