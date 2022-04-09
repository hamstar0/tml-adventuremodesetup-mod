using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public static string GetModFullFilePath( string modName, string extension, out string modFileNameExt ) {
			modFileNameExt = modName + extension;
			return Main.SavePath
				+ Path.DirectorySeparatorChar
				+ "Mods"
				+ Path.DirectorySeparatorChar
				+ modFileNameExt;
		}


		////////////////

		private LoadStatus GetModStatusFlags( string modName ) {
			LoadStatus statusFlags = 0;

			string fullFilePath = AMSMod.GetModFullFilePath( modName, ".tmod", out string modFileNameExt );

			//

			if( !File.Exists(fullFilePath) ) {
				statusFlags |= LoadStatus.MissingExternally;	//"Mod file already exists.";
			}

			//

			if( !this.FileExists(modFileNameExt) ) {
				statusFlags |= LoadStatus.MissingInternally;	//"Installer is missing mod file internally.";
			}

			//

			statusFlags |= ModLoader.Mods.Any( m => m.Name == modName )
				? LoadStatus.Loaded  //"Success.";
				: LoadStatus.Unloaded;

			return statusFlags;
		}


		////////////////

		private bool UnpackMod_If( string modName ) {
			LoadStatus statusFlags = this.GetModStatusFlags( modName );

			if( (statusFlags & LoadStatus.MissingExternally) == 0 ) {
				return false;
			}

			//

			string fullFilePath = AMSMod.GetModFullFilePath( modName, ".tmod", out string modFileNameExt );

			//

			byte[] modFileData = this.GetFileBytes( modFileNameExt );

			File.WriteAllBytes( fullFilePath, modFileData );

			if( !File.Exists( fullFilePath ) ) {
				string err = $"Could not write mod file {modName}.";

				this.Logger.Error( err );
				throw new Exception( err );
			}

			//

			return true;
		}
	}
}