using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private bool LoadMod_If( string modFileName, out string result ) {
			string modFileNameExt = modFileName + ".tmod";
			string fullFilePath = Main.SavePath
				+Path.DirectorySeparatorChar
				+"Mods"
				+Path.DirectorySeparatorChar
				+modFileNameExt;

			//

			if( File.Exists(fullFilePath) ) {
				result = "Mod file already exists.";
				return false;
			}

			//

			if( !this.FileExists(modFileNameExt) ) {
				result = "Installer is missing mod file internally.";
				return false;
			}

			byte[] modFileData = this.GetFileBytes( modFileNameExt );

			//

			File.WriteAllBytes( fullFilePath, modFileData );

			if( !File.Exists(fullFilePath) ) {
				throw new Exception( $"Could not write mod file {modFileName}." );
			}

			//

			result = "Success.";
			return true;
		}
	}
}