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
		public static AMSMod Instance => ModContent.GetInstance<AMSMod>();



		////////////////
		public static void PremultiplyTexture( Texture2D texture ) {
			Color[] buffer = new Color[texture.Width * texture.Height];

			texture.GetData( buffer );

			for( int i = 0; i < buffer.Length; i++ ) {
				buffer[i] = Color.FromNonPremultiplied( buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A );
			}

			texture.SetData( buffer );
		}




		////////////////

		public Texture2D LogoTex { get; private set; }
		public Texture2D LogoGlowsTex { get; private set; }



		////////////////

		private UIInstallPromptDialog InstallPromptUI;

		////

		private ISet<ModInfo> OutdatedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> UnloadedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> MissingMods = new HashSet<ModInfo>();
		private ISet<string> ExtraMods = new HashSet<string>();

		////
		
		private bool HasPostAddRecipes = false;
		private bool HasPostSetupContent = false;
		private bool HasAddRecipeGroups = false;

		////

		private FieldInfo LogoRotationField;
		private FieldInfo LogoScaleField;



		////////////////

		public override void Load() {
			if( Main.dedServ ) {
				return;
			}

			//

			this.LogoTex = this.GetTexture( "logo" );
			AMSMod.PremultiplyTexture( this.LogoTex );

			this.LogoGlowsTex = this.GetTexture( "logoglows" );
			AMSMod.PremultiplyTexture( this.LogoGlowsTex );

			//

			this.InstallPromptUI = new UIInstallPromptDialog();

			//
			
			this.LogoRotationField = typeof(Main).GetField(
				"logoRotation",
				BindingFlags.Instance | BindingFlags.NonPublic
			);
			
			this.LogoScaleField = typeof(Main).GetField(
				"logoScale",
				BindingFlags.Instance | BindingFlags.NonPublic
			);

			//

			this.GetEachModStatus(
				gameModeModEntries: ModInfo.NeededMods,
				outdatedMods: out this.OutdatedMods,
				unloadedMods: out this.UnloadedMods,
				nonexistentMods: out this.MissingMods,
				extraMods: out this.ExtraMods
			);

			//
			
			On.Terraria.Main.DrawMenu += this.Main_DrawMenu_Inject;

			//

			this.HasAddRecipeGroups = false;
			this.HasPostAddRecipes = false;
			this.HasPostSetupContent = false;
		}


		////////////////

		public override void PostAddRecipes() {
			this.HasPostAddRecipes = true;
		}

		public override void PostSetupContent() {
			this.HasPostSetupContent = true;
		}

		public override void AddRecipeGroups() {
			this.HasAddRecipeGroups = true;
		}


		////////////////

		private bool _HasPrompted = false;

		private void Main_DrawMenu_Inject(
					On.Terraria.Main.orig_DrawMenu orig,
					Main self,
					GameTime gameTime ) {
			orig.Invoke( self, gameTime );

			//

			Main.spriteBatch.Begin();
			this.DrawSubLogo_If( Main.spriteBatch );
			Main.spriteBatch.End();

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this._HasPrompted ) {
					this._HasPrompted = true;

					this.OpenInstallPromptMenu_If(
						outdatedMods: this.OutdatedMods,
						missingMods: this.MissingMods,
						unloadedMods: this.UnloadedMods,
						extraMods: this.ExtraMods
					);
				}
			}
		}


		////////////////

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
				texture: this.LogoGlowsTex,
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