using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	class UIInstallPromptDialog : UIState {
		public static IList<string> GetFittedLines( string text, float maxWidth ) {
			string[] words = text.Split( ' ' );

			IList<string> lines = new List<string>();
			string currentLine = "";
			float currentLineWidth = 0f;

			foreach( string word in words ) {
				if( word == "\n" ) {
					currentLineWidth = 0f;

					lines.Add( currentLine );
					lines.Add( "" );
					currentLine = "";

					continue;
				}

				//

				Vector2 dim = Main.fontMouseText.MeasureString( word+" " );

				if( (currentLineWidth + dim.X) > maxWidth ) {
					currentLineWidth = 0f;

					lines.Add( currentLine );
					currentLine = "";
				}

				//

				currentLine += word + " ";
				currentLineWidth += dim.X;
			}

			if( currentLine.Length > 0 ) {
				lines.Add( currentLine );
			}

			//

			return lines;
		}



		////////////////

		private UIPanel DialogPanel;



		////////////////
		
		public UIInstallPromptDialog() : base() {
		}


		public override void OnActivate() {
			this.RemoveAllChildren();

			//

			this.DialogPanel = new UIPanel();
			this.DialogPanel.Top.Set( 220f, 0f );
			this.DialogPanel.HAlign = 0.5f;
			this.DialogPanel.MaxWidth.Set( 800f, 0f );
			this.DialogPanel.MinWidth.Set( 600f, 0f );
			this.DialogPanel.Width.Set( 0f, 0.8f );
			this.DialogPanel.Height.Set( -288f, 1f );
			this.Append( this.DialogPanel );

			//

			var okButton = new UITextPanel<string>( "OK" );
			okButton.Top.Set( -48f, 1f );
			okButton.Left.Set( -192f, 0.5f );
			okButton.Width.Set( 128f, 0f );
			this.DialogPanel.Append( okButton );

			var cancelButton = new UITextPanel<string>( "Cancel" );
			cancelButton.Top.Set( -48f, 1f );
			cancelButton.Left.Set( 64f, 0.5f );
			cancelButton.Width.Set( 128f, 0f );
			cancelButton.OnClick += (_, __) => {
				Main.menuMode = 0;
				Main.PlaySound( SoundID.MenuOpen, -1, -1, 1, 1f, 0f );
			};
			this.DialogPanel.Append( cancelButton );
		}


		////////////////

		public void UpdateMissingModsDisplayList( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			UIElement RenderMod( ModInfo modInfo ) {
				var elem = new UIText( modInfo.DisplayName );
				return elem;
			}

			//

			float top = 0f;

			//

			var welcomeText = new UIText( "Welcome to Adventure Mode!" );
			this.DialogPanel.Append( welcomeText );

			top += 32f;

			//

			if( missingMods.Count > 0 ) {
				welcomeText.SetText( welcomeText.Text+" To begin, the following mods will be installed:" );

				//

				var missingModListPanel = new UIList();
				missingModListPanel.Top.Set( top, 0f );
				missingModListPanel.Width.Set( -25f, 1f );
				missingModListPanel.Height.Set( 240f, 0f );
				missingModListPanel.AddRange( missingMods
					.OrderBy( m => m.Name )
					.Select( m => RenderMod(m) )
				);
				this.DialogPanel.Append( missingModListPanel );

				var scrollbar = new UIScrollbar();
				scrollbar.Top.Set( top + 8f, 0f );
				scrollbar.Left.Set( -24f, 1f );
				scrollbar.Height.Set( -16f, 1f );
				scrollbar.SetView( 100f, 1000f );
				scrollbar.HAlign = 0f;

				missingModListPanel.SetScrollbar( scrollbar );

				top += 248f;
			}

			//

			string text = $"{unloadedMods.Count} (of {ModInfo.NeededMods.Length}) mods will need to be enabled"
				+" to play this game mode. Your existing enabled mods will be backed up as the 'Pre AM Backup'"
				+" mod pack (see the Mods->Mod Packs menu)."
				+" \n "
				+"After installation, a list of available mod updates will appear. If any mods need updates,"
				+" download them then, reload your mods (via. Mods menu), and you're ready to play. Happy trails!";

			//

			float containerWidth = this.DialogPanel.GetDimensions().Width;
			containerWidth = containerWidth < 240f
				? 240f
				: containerWidth;

			IList<string> lines = UIInstallPromptDialog.GetFittedLines( text, containerWidth - 16f );

			//

			foreach( string line in lines ) {
				if( line != "" ) {
					var gameModeInfoText = new UIText( line );
					gameModeInfoText.Top.Set( top, 0f );
					this.DialogPanel.Append( gameModeInfoText );
				}

				top += 24f;
			}
		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			Main.MenuUI.Update( Main._drawInterfaceGameTime ?? new GameTime() );	// ?!

			//

			base.Draw( spriteBatch );
		}
	}
}