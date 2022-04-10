using System;
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
			if( Main.MenuUI.CurrentState == this.InstallPromptUI ) {
				return;
			}

			//

			var pos = new Vector2( Main.screenWidth / 2, 190 );

			int b = (255 + (Main.tileColor.R * 2)) / 3;
			Color dayColor = new Color( b, b, b, 255 );

			float rot = (float)this.LogoRotationField.GetValue( Main.instance );
			rot *= 0.5f;

			var origin = new Vector2( this.LogoTex.Width / 2, this.LogoTex.Height / 2 );

			float scale = (float)this.LogoScaleField.GetValue( Main.instance );
				//* 0.75f;

			//

			spriteBatch.Draw(
				texture: this.LogoTex,
				position: pos,
				sourceRectangle: null,
				color: dayColor,
				rotation: rot,
				origin: origin,
				scale: scale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			//

			float glowPerc = 0.8f + (0.2f * Main.rand.NextFloat());
			Color glowColor = Color.White * glowPerc;
			
			for( int i=0; i<this.LogoGlowIconTexs.Length; i++ ) {
				spriteBatch.Draw(
					texture: this.LogoGlowIconTexs[i],
					position: pos,
					sourceRectangle: null,
					color: glowColor,
					rotation: rot,
					origin: origin,
					scale: scale,
					effects: SpriteEffects.None,
					layerDepth: 0f
				);

				glowColor = Color.Lerp( glowColor, dayColor, 0.5f );
			}

			//

			for( int i=0; i<this.LogoGlowTexs.Length; i++ ) {
				spriteBatch.Draw(
					texture: this.LogoGlowTexs[i],
					position: pos,
					sourceRectangle: null,
					color: Color.White * Main.rand.NextFloat(),
					rotation: rot,
					origin: origin,
					scale: scale,
					effects: SpriteEffects.None,
					layerDepth: 0f
				);
			}

			//

			Vector2 bonus = Main.DrawThickCursor( false );
			Main.DrawCursor( bonus, false );
		}
	}
}