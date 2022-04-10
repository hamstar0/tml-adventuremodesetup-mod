using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private Texture2D MyLogo1Override = null;
		private Texture2D MyLogo2Override = null;



		////////////////
		
		private void LoadLogo() {
			bool isNotDisposed = !this.LogoTex.IsDisposed
				&& !this.LogoGlowIconTexs.Any( t => t?.IsDisposed ?? true )
				&& !this.LogoGlowTexs.Any( t => t?.IsDisposed ?? true );
			if( !isNotDisposed ) {
				return;
			}

			//

			if( this.MyLogo1Override == null ) {
				this.MyLogo1Override = Main.logoTexture;
				this.MyLogo2Override = Main.logo2Texture;

				//

				Main.instance.LoadProjectile( ProjectileID.ShadowBeamHostile );

				Main.logoTexture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
				Main.logo2Texture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
			}

		}

		private void UnloadLogo() {
			if( this.MyLogo1Override != null ) {
				Main.logoTexture = this.MyLogo1Override;
				Main.logo2Texture = this.MyLogo2Override;
			}
		}


		////////////////

		private void DrawFullLogo_If( SpriteBatch spriteBatch ) {
			if( !Main.gameMenu ) {
				return;
			}
			if( Main.MenuUI.CurrentState == this.InstallPromptUI ) {
				return;
			}
			if( ModLoader.GetMod("AdventureMode") == null ) {
				return;
			}

			//

			bool isNotDisposed = !this.LogoTex.IsDisposed
				&& !this.LogoGlowIconTexs.Any( t => t?.IsDisposed ?? true )
				&& !this.LogoGlowTexs.Any( t => t?.IsDisposed ?? true );
			if( !isNotDisposed ) {
				return;
			}

			//

			float rot = (float)this.LogoRotationField.GetValue( Main.instance );
			float scale = (float)this.LogoScaleField.GetValue( Main.instance );

			int dayShade = ( 255 + ( Main.tileColor.R * 2 ) ) / 3;
			Color dayColor = new Color( dayShade, dayShade, dayShade, 255 );

			//

			this.DrawMainLogo( spriteBatch, dayColor, rot, scale );
			this.DrawSubLogo( spriteBatch, dayColor, rot, scale );
		}

			
		private void DrawMainLogo( SpriteBatch spriteBatch, Color baseColor, float baseRotation, float baseScale ) {
			var pos = new Vector2( Main.screenWidth / 2, 100 );
			var origin = new Vector2( this.MyLogo1Override.Width / 2, this.MyLogo1Override.Height / 2 );

			float rot = (float)this.LogoRotationField.GetValue( Main.instance );
			float scale = (float)this.LogoScaleField.GetValue( Main.instance );

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
				texture: this.MyLogo1Override,
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
				texture: this.MyLogo2Override,
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

				glowColor = Color.Lerp( glowColor, baseColor, 0.5f );
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