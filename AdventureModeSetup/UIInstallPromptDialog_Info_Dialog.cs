using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private UIPanel CreateDialog() {
			float offsetY = 32f;	//220f;

			var dialogPanel = new UIPanel();
			dialogPanel.Top.Set( offsetY, 0f );
			dialogPanel.HAlign = 0.5f;
			//dialogPanel.MaxWidth.Set( 800f, 0f );
			//dialogPanel.MinWidth.Set( 600f, 0f );
			//dialogPanel.Width.Set( 0f, 0.8f );
			dialogPanel.Width.Set( 800, 0f );
			dialogPanel.Height.Set( -(offsetY + 4f), 1f );
			this.Append( dialogPanel );

			{
				var okButton = new UITextPanel<string>( "Install" );
				var cancelButton = new UITextPanel<string>( "Cancel" );

				Color unlit = okButton.BackgroundColor;
				Color lit = unlit * 1.25f;

				//

				okButton.Top.Set( -40f, 1f );
				okButton.Left.Set( -256f, 1f );
				okButton.Width.Set( 128f, 0f );
				okButton.OnClick += ( _, __ ) => this.ConfirmInstall();
				okButton.OnMouseOver += ( _, __ ) => okButton.BackgroundColor = lit;
				okButton.OnMouseOut += ( _, __ ) => okButton.BackgroundColor = unlit;
				dialogPanel.Append( okButton );

				//

				cancelButton.Top.Set( -40f, 1f );
				cancelButton.Left.Set( -128f, 1f );
				cancelButton.Width.Set( 128f, 0f );
				cancelButton.OnClick += ( _, __ ) => this.CancelInstall();
				cancelButton.OnMouseOver += ( _, __ ) => cancelButton.BackgroundColor = lit;
				cancelButton.OnMouseOut += ( _, __ ) => cancelButton.BackgroundColor = unlit;
				dialogPanel.Append( cancelButton );
			}

			//

			return dialogPanel;
		}


		////////////////

		private void ConfirmInstall() {
			var mymod = AMSMod.Instance;

			mymod.UnpackMods_Current();
			mymod.BackupEnabledMods();
			mymod.EnableMods_Current();

			//

			Main.PlaySound( SoundID.MenuClose, -1, -1, 1, 1f, 0f );

			//

			mymod.ClearRecordedModStates();

			//

			////ModLoader.Reload();
			//MethodInfo reloadMethodInfo = typeof(ModLoader)
			//	.GetMethod("Reload", BindingFlags.NonPublic | BindingFlags.Static );
			//
			//reloadMethodInfo.Invoke( null, new object[0] );

			//
			
			Main.menuMode = 10006;  // reload mods 'menu'
			//Main.menuMode = 10007;	// mod browser
		}

		private void CancelInstall() {
			AMSMod.Instance.ClearRecordedModStates();

			//

			Main.menuMode = 0;

			Main.PlaySound( SoundID.MenuClose, -1, -1, 1, 1f, 0f );
		}
	}
}