using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private Texture2D MyLogo1Backup = null;
		private Texture2D MyLogo2Backup = null;



		////////////////
		
		private void LoadLogo() {
			this.LogoTex = this.GetTexture( "logo" );
			AMSMod.PremultiplyTexture( this.LogoTex );

			for( int i = 0; i < this.LogoGlowIconTexs.Length; i++ ) {
				this.LogoGlowIconTexs[i] = this.GetTexture( $"logoglowicon{(i + 1)}" );
				AMSMod.PremultiplyTexture( this.LogoGlowIconTexs[i] );
			}

			for( int i = 0; i < this.LogoGlowTexs.Length; i++ ) {
				this.LogoGlowTexs[i] = this.GetTexture( $"logoglow{(i + 1)}" );
				AMSMod.PremultiplyTexture( this.LogoGlowTexs[i] );
			}

			//

			if( this.MyLogo1Backup == null ) {
				this.MyLogo1Backup = Main.logoTexture;
				this.MyLogo2Backup = Main.logo2Texture;

				//

				Main.instance.LoadProjectile( ProjectileID.ShadowBeamHostile );

				Main.logoTexture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
				Main.logo2Texture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
			}
		}

		private void UnloadLogo() {
			if( this.MyLogo1Backup != null ) {
				Main.logoTexture = this.MyLogo1Backup;
				Main.logo2Texture = this.MyLogo2Backup;
			}
		}


		////////////////

		private bool CanDrawSubLogo() {
			return Main.gameMenu 
				&& Main.MenuUI.CurrentState != this.InstallPromptUI
				&& Main.menuMode != 10006	// safeguard against mod reload 'menu'?
				&& ModLoader.GetMod("AdventureMode") != null;
		}


		////////////////

		private void DrawFullLogo_If( SpriteBatch spriteBatch ) {
			float rot = (float)this.LogoRotationField.GetValue( Main.instance );
			float scale = (float)this.LogoScaleField.GetValue( Main.instance );

			int dayShade = ( 255 + ( Main.tileColor.R * 2 ) ) / 3;
			Color dayColor = new Color( dayShade, dayShade, dayShade, 255 );

			//

			this.DrawMainLogo( spriteBatch, dayColor, rot, scale );

			if( this.CanDrawSubLogo() ) {
				this.DrawSubLogo_If( spriteBatch, dayColor, rot, scale );
			}
		}


		////////////////

		private void DrawSubLogo_If( SpriteBatch spriteBatch, Color baseColor, float baseRotation, float baseScale ) {
			bool isDisposed = this.LogoTex.IsDisposed
				|| this.LogoGlowIconTexs.Any( t => t?.IsDisposed ?? true )
				|| this.LogoGlowTexs.Any( t => t?.IsDisposed ?? true );

			if( isDisposed ) {
				return;
			}

			//

			this.DrawSubLogo( spriteBatch, baseColor, baseRotation, baseScale );
		}

			
		private void DrawMainLogo( SpriteBatch spriteBatch, Color baseColor, float baseRotation, float baseScale ) {
			var pos = new Vector2( Main.screenWidth / 2, 100 );
			var origin = new Vector2( this.MyLogo1Backup.Width / 2, this.MyLogo1Backup.Height / 2 );

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
				texture: this.MyLogo1Backup,
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
				texture: this.MyLogo2Backup,
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