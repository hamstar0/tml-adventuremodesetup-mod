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
			Unloaded = 2,
			MissingInternally = 4,
			MissingExternally = 8
		}



		private LoadStatus GetModStatusFlags( string modName ) {
			LoadStatus statusFlags = 0;

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

			statusFlags |= ModLoader.Mods.Any( m => m.Name == modName )
				? LoadStatus.Loaded  //"Success.";
				: LoadStatus.Unloaded;

			return statusFlags;
		}


		////////////////

		private void GetEachModStatus(
					IEnumerable<ModInfo> gameModeModEntries,
					out ISet<ModInfo> unloadedMods,
					out ISet<ModInfo> nonexistentMods ) {
			unloadedMods = new HashSet<ModInfo>();
			nonexistentMods = new HashSet<ModInfo>();

			foreach( ModInfo modInfo in gameModeModEntries ) {
				LoadStatus statusFlags = this.GetModStatusFlags( modInfo.Name );

				//

				if( statusFlags == LoadStatus.Loaded ) {
					continue;
				}

				//

				if( (statusFlags & LoadStatus.MissingInternally) == LoadStatus.MissingInternally ) {
					this.Logger.Warn( $"Adventure Mode Setup - {modInfo.Name} install failed:"
						+ " Missing mod file internally" );
				}

				//

				if( (statusFlags & LoadStatus.Unloaded) == LoadStatus.Unloaded ) {
					unloadedMods.Add( modInfo );
				}
				if( (statusFlags & LoadStatus.MissingExternally) == LoadStatus.MissingExternally ) {
					nonexistentMods.Add( modInfo );
				}
			}
		}
	}
}