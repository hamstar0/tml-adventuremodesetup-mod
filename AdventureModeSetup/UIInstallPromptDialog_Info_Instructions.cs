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

		private IEnumerable<(UIElement elem, float height)> CreateInfoElements(
					ISet<ModInfo> missingMods,
					ISet<ModInfo> unloadedMods ) {
			var elements = new List<(UIElement, float)>();

			//

			void StoreElementsFromTexts( IEnumerable<string> mylines ) {
				foreach( string line in mylines ) {
					elements.Add( (new UIText(line), 24f) );
				}
			}

			//

			//int activeMods = ModInfo.NeededMods.Length - unloadedMods.Count;
			string text1 = $"{ModInfo.NeededMods.Length} mods will need to be enabled to play this game mode. Any"
				+$" existing enabled mods will be backed up as the '{AMSMod.BackupFileBaseName}' mod pack (see"
				+$" the Mods -> Mod Packs menu).";
			string text2 = $"Finally, reload your mods (via. Mods menu), and you're ready to play. Be sure to"
				+$" check if any mod updates exist in the Mod Browser menu, if you want. Happy trails!";

			//

			float containerWidth = this.DialogPanel.GetDimensions().Width;
			containerWidth = Math.Max( 240f, containerWidth - 8f );

			//

			IList<string> lines1 = UIInstallPromptDialog.GetFittedLines( text1, containerWidth );
			StoreElementsFromTexts( lines1 );

			//

			UIImage img1 = new UIImage( AMSMod.Instance.GetTexture("Instruction1") );
			img1.ImageScale = 0.5f;
			img1.MarginLeft = -152f;
			img1.MarginTop = -24f;
			img1.SetPadding( 0f );

			elements.Add( (img1, 52f) );

			//

			IList<string> lines2 = UIInstallPromptDialog.GetFittedLines( text2, containerWidth );
			StoreElementsFromTexts( lines2 );

			//

			UIImage img2 = new UIImage( AMSMod.Instance.GetTexture("Instruction2") );
			img2.ImageScale = 0.5f;
			img2.MarginLeft = -152f;
			img2.MarginTop = -24f;
			img2.SetPadding( 0f );

			elements.Add( (img2, 52f) );

			//

			return elements;
		}
	}
}