using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;


namespace AdventureModeSetup {
	public partial class AdventureModeLogo {
		private void DrawSubLogo( SpriteBatch spriteBatch, Color baseColor, float baseRotation, float baseScale ) {
			var pos = new Vector2( Main.screenWidth / 2, 190 );

			float rot = baseRotation * 0.5f;

			var origin = new Vector2( this.LogoTex.Width / 2, this.LogoTex.Height / 2 );

			float scale = baseScale;	//* 0.75f;

			//

			spriteBatch.Draw(
				texture: this.LogoTex,
				position: pos,
				sourceRectangle: null,
				color: baseColor,
				rotation: rot,
				origin: origin,
				scale: scale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			//

			float pulse = (float)Main.mouseTextColor / 255f;
			Color glowColorHi = Color.White;
			Color glowColorLo = Color.White * pulse * pulse * 0.75f;
			
			for( int i=0; i<this.LogoGlowIconTexs.Length; i++ ) {
				spriteBatch.Draw(
					texture: this.LogoGlowIconTexs[i],
					position: pos,
					sourceRectangle: null,
					color: glowColorHi,
					rotation: rot,
					origin: origin,
					scale: scale,
					effects: SpriteEffects.None,
					layerDepth: 0f
				);

				glowColorHi = glowColorLo;	//Color.Lerp( glowColorHi, glowColorLo, 0.5f );
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