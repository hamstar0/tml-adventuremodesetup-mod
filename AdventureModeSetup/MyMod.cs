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
		public Texture2D[] LogoGlowIconTexs { get; private set; } = new Texture2D[2];
		public Texture2D[] LogoGlowTexs { get; private set; } = new Texture2D[6];



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

			for( int i=0; i<this.LogoGlowIconTexs.Length; i++ ) {
				this.LogoGlowIconTexs[i] = this.GetTexture( "logoglowicon"+(i+1) );
				AMSMod.PremultiplyTexture( this.LogoGlowIconTexs[i] );
			}
			
			for( int i=0; i<this.LogoGlowTexs.Length; i++ ) {
				this.LogoGlowTexs[i] = this.GetTexture( "logoglow"+(i+1) );
				AMSMod.PremultiplyTexture( this.LogoGlowTexs[i] );
			}

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
	}
}