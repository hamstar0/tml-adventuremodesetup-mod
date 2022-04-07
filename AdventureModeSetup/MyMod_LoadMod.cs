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
			Loaded = 0,
			Unloaded = 1,
			MissingInternally = 2,
			MissingExternally = 4
		}



		private LoadStatus GetModStatusFlags( string modName ) {
			LoadStatus statusFlags = LoadStatus.Loaded;  //"Success.";

			string modFileNameExt = modName + ".tmod";
			string fullFilePath = Main.SavePath
				+Path.DirectorySeparatorChar
				+"Mods"
				+Path.DirectorySeparatorChar
				+modFileNameExt;

			//

			if( !File.Exists(fullFilePath) ) {
				statusFlags |= LoadStatus.MissingExternally;	//"Mod file already exists.";
			}

			//

			if( !this.FileExists(modFileNameExt) ) {
				statusFlags |= LoadStatus.MissingInternally;	//"Installer is missing mod file internally.";
			}

			//byte[] modFileData = this.GetFileBytes( modFileNameExt );
			//
			//
			//
			//File.WriteAllBytes( fullFilePath, modFileData );
			//
			//if( !File.Exists(fullFilePath) ) {
			//	throw new Exception( $"Could not write mod file {modFileName}." );
			//}

			//

			if( !ModLoader.Mods.Any(m => m.Name == modName) ) {
				statusFlags |= LoadStatus.Unloaded;
			}

			return statusFlags;
		}


		////////////////

		private void GetEachModStatus(
					IEnumerable<string> gameModeMods,
					out ISet<string> unloadedMods,
					out ISet<string> nonexistentMods ) {
			unloadedMods = new HashSet<string>();
			nonexistentMods = new HashSet<string>();

			foreach( string modName in gameModeMods ) {
				if( ModLoader.Mods.Any( m => m.Name == modName ) ) {
					//this.Logger.Info( $"Adventure Mode Setup - {modName} already installed and running." );
					continue;
				}

				//

				LoadStatus statusFlags = this.GetModStatusFlags( modName );

				if( (statusFlags & LoadStatus.Loaded) == LoadStatus.Loaded ) {
					continue;
				}

				//

				if( (statusFlags & LoadStatus.MissingInternally) == LoadStatus.MissingInternally ) {
					this.Logger.Warn( $"Adventure Mode Setup - {modName} install failed:"
						+ " Missing mod file internally" );
				}

				//

				if( (statusFlags & LoadStatus.Unloaded) == LoadStatus.Unloaded ) {
					unloadedMods.Add( modName );
				}
				if( (statusFlags & LoadStatus.MissingExternally) == LoadStatus.MissingExternally ) {
					nonexistentMods.Add( modName );
				}
			}
		}
	}
}