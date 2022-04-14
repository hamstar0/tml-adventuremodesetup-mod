using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;


namespace AdventureModeSetup {
	public partial class AdventureModeLogo {
		private void DrawMainLogo( SpriteBatch spriteBatch, Color baseColor, float baseRotation, float baseScale ) {
			var pos = new Vector2( Main.screenWidth / 2, 100 );
			var origin = new Vector2( this.MainLogo1Backup.Width / 2, this.MainLogo1Backup.Height / 2 );

			float logoAShade = (float)Main.LogoA / 255f;
			float logoBShade = (float)Main.LogoB / 255f;

			Color color1 = new Color(
				(int)( (float)baseColor.R * logoAShade ),
				(int)( (float)baseColor.G * logoAShade ),
				(int)( (float)baseColor.B * logoAShade ),
				(int)( (float)baseColor.A * logoAShade )
			);
			Color color2 = new Color(
				(byte)( (float)baseColor.R * logoBShade ),
				(byte)( (float)baseColor.G * logoBShade ),
				(byte)( (float)baseColor.B * logoBShade ),
				(byte)( (float)baseColor.A * logoBShade )
			);

			//

			spriteBatch.Draw(
				texture: this.MainLogo1Backup,
				position: pos,
				sourceRectangle: null,
				color: color1,
				rotation: baseRotation,
				origin: origin,
				scale: baseScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
			spriteBatch.Draw(
				texture: this.MainLogo2Backup,
				position: pos,
				sourceRectangle: null,
				color: color2,
				rotation: baseRotation,
				origin: origin,
				scale: baseScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}