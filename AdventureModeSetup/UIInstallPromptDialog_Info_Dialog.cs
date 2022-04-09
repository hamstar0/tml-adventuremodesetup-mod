using System;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
		private UIPanel CreateDialog() {
			var dialogPanel = new UIPanel();
			dialogPanel.Top.Set( 220f, 0f );
			dialogPanel.HAlign = 0.5f;
			//dialogPanel.MaxWidth.Set( 800f, 0f );
			//dialogPanel.MinWidth.Set( 600f, 0f );
			//dialogPanel.Width.Set( 0f, 0.8f );
			dialogPanel.Width.Set( 800, 0f );
			dialogPanel.Height.Set( -224f, 1f );
			this.Append( dialogPanel );

			{
				var okButton = new UITextPanel<string>( "OK" );
				okButton.Top.Set( -40f, 1f );
				okButton.Left.Set( -256f, 1f );
				okButton.Width.Set( 128f, 0f );
				okButton.OnClick += ( _, __ ) => {
					var mymod = AMSMod.Instance;
					mymod.UnpackMods( ModInfo.NeededMods );
					mymod.BackupEnabledMods();
					mymod.EnableMods( ModInfo.NeededMods );

					Main.menuMode = 10007;

					Main.PlaySound( SoundID.MenuOpen, -1, -1, 1, 1f, 0f );
				};
				dialogPanel.Append( okButton );

				//

				var cancelButton = new UITextPanel<string>( "Cancel" );
				cancelButton.Top.Set( -40f, 1f );
				cancelButton.Left.Set( -128f, 1f );
				cancelButton.Width.Set( 128f, 0f );
				cancelButton.OnClick += ( _, __ ) => {
					Main.menuMode = 0;

					Main.PlaySound( SoundID.MenuOpen, -1, -1, 1, 1f, 0f );
				};
				dialogPanel.Append( cancelButton );
			}

			//

			return dialogPanel;
		}
	}
}