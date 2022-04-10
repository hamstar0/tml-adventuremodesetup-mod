using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private void DrawSubLogo_If( SpriteBatch spriteBatch ) {
			if( !Main.gameMenu ) {
				return;
			}

			//

			int b = (255 + (Main.tileColor.R * 2)) / 3;
			Color dayColor = new Color( b, b, b, 255 );

			//

			spriteBatch.Draw(
				texture: this.LogoTex,
				position: new Vector2( Main.screenWidth / 2, 168 ),
				sourceRectangle: null,
				color: dayColor,
				rotation: (float)this.LogoRotationField.GetValue( Main.instance ),
				origin: new Vector2( this.LogoTex.Width / 2, this.LogoTex.Height / 2 ),
				scale: (float)this.LogoScaleField.GetValue( Main.instance ),
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			spriteBatch.Draw(
				texture: this.LogoGlow1Tex,
				position: new Vector2( Main.screenWidth / 2, 168 ),
				sourceRectangle: null,
				color: Color.White * Main.rand.NextFloat(),
				rotation: (float)this.LogoRotationField.GetValue( Main.instance ),
				origin: new Vector2( this.LogoTex.Width / 2, this.LogoTex.Height / 2 ),
				scale: (float)this.LogoScaleField.GetValue( Main.instance ),
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			//

			Vector2 bonus = Main.DrawThickCursor( false );
			Main.DrawCursor( bonus, false );
		}
	}
}