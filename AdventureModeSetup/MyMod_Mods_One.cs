using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public enum LoadStatus {
			Loaded = 1,
			Deactivated = 2,
			MissingInternally = 4,
			MissingExternally = 8,
			Outdated = 16
		}



		////////////////

		public static string GetModFullFilePath( string modName, out string modFileNameExt ) {
			modFileNameExt = modName + ".tmod";
			return Main.SavePath
				+ Path.DirectorySeparatorChar
				+ "Mods"
				+ Path.DirectorySeparatorChar
				+ modFileNameExt;
		}



		////////////////

		private LoadStatus GetModStatusFlags( string modName ) {
			LoadStatus statusFlags = 0;

			string fullFilePath = AMSMod.GetModFullFilePath( modName, out string modFileNameExt );

			string internalFilePath = $"ModFiles/{modFileNameExt}";

			//

			if( !File.Exists(fullFilePath) ) {
				statusFlags |= LoadStatus.MissingExternally;	//"Mod file already exists.";
			}

			//

			if( !this.FileExists(internalFilePath) ) {
				statusFlags |= LoadStatus.MissingInternally;	//"Installer is missing mod file internally.";
			}

			//

			statusFlags |= ModLoader.Mods.Any( m => m.Name == modName )
				? LoadStatus.Loaded  //"Success.";
				: LoadStatus.Deactivated;

			//

			Mod currMod = ModLoader.GetMod( modName );

			if( currMod != null ) {
				ModInfo currModInfo = ModInfo.AdventureModeMods.FirstOrDefault( mi => mi.Name == modName );

				if( currMod.Version < currModInfo.MinVersion ) {
					statusFlags |= LoadStatus.Outdated;
				}
			}

			//

			return statusFlags;
		}
	}
}