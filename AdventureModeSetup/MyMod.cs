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
			this.LoadTimer();

			//

			if( Main.netMode == NetmodeID.Server || Main.dedServ ) {
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

			//

			this.UnloadTimer();

			//

			this.LogoTex = null;
			this.LogoGlowIconTexs = null;
			this.LogoGlowTexs = null;
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
			this.DrawFullLogo_If( Main.spriteBatch );

			//
			
			orig.Invoke( self, gameTime );

			//

			if( this.HasPostAddRecipes && this.HasPostSetupContent && this.HasAddRecipeGroups ) {
				if( !this._HasPrompted ) {
					this._HasPrompted = true;

					this.RunAfterTimer( 2, () => {
						this.OpenInstallPromptMenu_If(
							outdatedMods: this.OutdatedMods,
							missingMods: this.MissingMods,
							deactivatedMods: this.DeactivatedMods,
							extraMods: this.ExtraMods
						);
					} );
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