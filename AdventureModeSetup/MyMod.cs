using System;
using System.Collections.Generic;
using System.Linq;
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
		private ISet<ModInfo> DeactivatedMods = new HashSet<ModInfo>();
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

			this.LoadLogo();

			//
			
			var mostAccess = BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.Static;
			Type sbType = typeof(SpriteBatch);

			//

			this.InstallPromptUI = new UIInstallPromptDialog();

			//

			this.LogoRotationField = typeof(Main).GetField( "logoRotation", mostAccess );
			this.LogoScaleField = typeof(Main).GetField( "logoScale", mostAccess );

			//

			this.GetEachModStatus(
				gameModeModEntries: ModInfo.AdventureModeMods,
				outdatedMods: out this.OutdatedMods,
				deactivatedMods: out this.DeactivatedMods,
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

		public override void Unload() {
			this.UnloadLogo();
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
			//Main.spriteBatch.Begin();
			this.DrawFullLogo_If( Main.spriteBatch );
			//Main.spriteBatch.End();

			//

			try {
				orig.Invoke( self, gameTime );
			} catch {
				var mostAccess = BindingFlags.Public |
					BindingFlags.NonPublic |
					BindingFlags.Instance |
					BindingFlags.Static;

				Type sbType = typeof( SpriteBatch );

				FieldInfo sbBegunField = sbType.GetField( "inBeginEndPair", mostAccess );
				if( sbBegunField == null ) {
					sbBegunField = sbType.GetField( "_beginCalled", mostAccess );
				}
				if( sbBegunField == null ) {
					sbBegunField = sbType.GetField( "beginCalled", mostAccess );
				}

				//

				if( (bool)sbBegunField.GetValue(Main.spriteBatch) ) {
					Main.spriteBatch.End();
				}
			}

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this._HasPrompted ) {
					this._HasPrompted = true;

					this.OpenInstallPromptMenu_If(
						outdatedMods: this.OutdatedMods,
						missingMods: this.MissingMods,
						deactivatedMods: this.DeactivatedMods,
						extraMods: this.ExtraMods
					);
				}
			}
		}


		////////////////

		public void ClearRecordedModStates() {
			this.OutdatedMods.Clear();
			this.MissingMods.Clear();
			this.DeactivatedMods.Clear();
			this.ExtraMods.Clear();
		}
	}
}