using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AdventureModeLogo {
		public Texture2D LogoTex { get; private set; }
		public Texture2D[] LogoGlowIconTexs { get; private set; } = new Texture2D[2];
		public Texture2D[] LogoGlowTexs { get; private set; } = new Texture2D[6];


		////////////////

		private FieldInfo LogoRotationField;
		private FieldInfo LogoScaleField;


		////////////////

		private Texture2D MainLogo1Backup = null;
		private Texture2D MainLogo2Backup = null;



		////////////////
		
		internal AdventureModeLogo() {
			var mymod = AMSMod.Instance;

			this.LogoTex = mymod.GetTexture( "logo" );
			AMSMod.PremultiplyTexture( this.LogoTex );

			for( int i = 0; i < this.LogoGlowIconTexs.Length; i++ ) {
				this.LogoGlowIconTexs[i] = mymod.GetTexture( $"logoglowicon{(i + 1)}" );
				AMSMod.PremultiplyTexture( this.LogoGlowIconTexs[i] );
			}

			for( int i = 0; i < this.LogoGlowTexs.Length; i++ ) {
				this.LogoGlowTexs[i] = mymod.GetTexture( $"logoglow{(i + 1)}" );
				AMSMod.PremultiplyTexture( this.LogoGlowTexs[i] );
			}

			//

			if( this.MainLogo1Backup == null ) {
				this.MainLogo1Backup = Main.logoTexture;
				this.MainLogo2Backup = Main.logo2Texture;

				//

				Main.instance.LoadProjectile( ProjectileID.ShadowBeamHostile );

				Main.logoTexture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
				Main.logo2Texture = Main.projectileTexture[ProjectileID.ShadowBeamHostile];
			}

			//

			var mostAccess = BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.Static;
			Type sbType = typeof( SpriteBatch );

			//

			this.LogoRotationField = typeof( Main ).GetField( "logoRotation", mostAccess );
			this.LogoScaleField = typeof( Main ).GetField( "logoScale", mostAccess );
		}


		public void Unload() {
			if( this.MainLogo1Backup != null ) {
				Main.logoTexture = this.MainLogo1Backup;
				Main.logo2Texture = this.MainLogo2Backup;
			}
		}


		////////////////

		public bool CanDrawSubLogo() {
			return Main.gameMenu
				//&& Main.MenuUI.CurrentState != this.InstallPromptUI
				&& Main.menuMode != 10006	// safeguard against mod reload 'menu' crash?
				&& ModLoader.GetMod("AdventureMode") != null
				&& Main.MenuUI.CurrentState == null
				&& AMSMod.Instance != null;
		}
	}
}