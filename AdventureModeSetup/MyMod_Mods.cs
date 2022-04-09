using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public enum LoadStatus {
			Loaded = 1,
			Unloaded = 2,
			MissingInternally = 4,
			MissingExternally = 8
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


		////////////////

		internal void UnpackMods( ModInfo[] gameModeModInfos ) {
			foreach( ModInfo modInfo in gameModeModInfos ) {
				this.UnpackMod_If( modInfo.Name );
			}
		}


		////////////////

		internal void EnableMods( ModInfo[] gameModeModInfos ) {
			string modsPath = Main.SavePath
				+ Path.DirectorySeparatorChar
				+ "Mods";
			string modListJsonFullPath = modsPath
				+ Path.DirectorySeparatorChar
				+ "enabled.json";

			//

			string[] modInfosArr = gameModeModInfos.Select( m => m.Name ).ToArray();
			string dataJson = JsonConvert.SerializeObject( modInfosArr, new JsonSerializerSettings() );

			File.WriteAllText( modListJsonFullPath, dataJson );
		}


		////////////////

		internal void BackupEnabledMods() {
			string modsPath = Main.SavePath
				+ Path.DirectorySeparatorChar
				+ "Mods";
			string modListJsonFullPath = modsPath
				+ Path.DirectorySeparatorChar
				+ "enabled.json";
			string modPacksPath = modsPath
				+ Path.DirectorySeparatorChar
				+ "ModPacks"
				+ Path.DirectorySeparatorChar;

			//

			byte[] buf = FileUtilities.ReadAllBytes( modListJsonFullPath, false );
			string enabledJson = System.Text.Encoding.UTF8.GetString( buf );

			//

			string backupPath = $"{modPacksPath}Pre AM Mods.json";

			for( int i=0; FileUtilities.Exists(backupPath, false); i++ ) {
				backupPath = $"{modPacksPath}Pre AM Mods ({i}).json";
			}

			File.WriteAllText( modListJsonFullPath, enabledJson );
		}
	}
}