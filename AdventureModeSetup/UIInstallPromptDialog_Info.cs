using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;


namespace AdventureModeSetup {
	partial class UIInstallPromptDialog : UIState {
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

				Vector2 dim = Main.fontMouseText.MeasureString( word + " " );

				if( ( currentLineWidth + dim.X ) > maxWidth ) {
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

		private IEnumerable<UIElement> CreateInfoElements( ISet<ModInfo> missingMods, ISet<ModInfo> unloadedMods ) {
			var list = new List<UIElement>();

			//

			//int activeMods = ModInfo.NeededMods.Length - unloadedMods.Count;
			string text = $"{unloadedMods.Count} mods will need to be enabled to play this game mode. Your"
				+ " existing enabled mods will be backed up as the 'Pre AM Backup' mod pack (see the Mods->Mod"
				+ " Packs menu)."
				+ " \n "
				+ "After installation, a list of available mod updates will appear. If any mods need updates,"
				+ " download them then, reload your mods (via. Mods menu), and you're ready to play. Happy trails!";

			//

			float containerWidth = this.DialogPanel.GetDimensions().Width;
			containerWidth = containerWidth < 240f
				? 240f
				: containerWidth;

			IList<string> lines = UIInstallPromptDialog.GetFittedLines( text, containerWidth - 16f );

			//

			foreach( string line in lines ) {
				if( line != "" ) {
					list.Add( new UIText(line) );
				} else {
					list.Add( null );
				}
			}

			return list;
		}
	}
}