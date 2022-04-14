using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;


namespace AdventureModeSetup {
	public partial class AdventureModeLogo {
		public bool DrawFullLogo_If( SpriteBatch spriteBatch ) {
			bool isDisposed = (this.LogoTex?.IsDisposed ?? true)
				|| (this.MainLogo1Backup?.IsDisposed ?? true)
				|| (this.MainLogo2Backup?.IsDisposed ?? true)
				|| this.LogoGlowIconTexs.Any( t => t?.IsDisposed ?? true )
				|| this.LogoGlowTexs.Any( t => t?.IsDisposed ?? true );

			if( isDisposed ) {
				return false;
			}

			//

			var mymod = AMSMod.Instance;

			float rot = (float)this.LogoRotationField.GetValue( Main.instance );
			float scale = (float)this.LogoScaleField.GetValue( Main.instance );

			int dayShade = (255 + (Main.tileColor.R * 2)) / 3;
			Color dayColor = new Color( dayShade, dayShade, dayShade, 255 );

			//

			this.DrawMainLogo( spriteBatch, dayColor, rot, scale );

			if( this.CanDrawSubLogo() ) {
				if( !mymod.IsTimerRunning() ) {
					this.DrawSubLogo( spriteBatch, dayColor, rot, scale );
				}
			} else {
				mymod.RunAfterTimer( 2, () => { } );
			}

			//

			return true;
		}
	}
}