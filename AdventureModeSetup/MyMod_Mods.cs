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
			char div = Path.DirectorySeparatorChar;
			string modsFolder = Main.SavePath
				+ div
				+ "Mods";
			string modListJsonFullPath = modsFolder
				+ div
				+ "enabled.json";

			//

			//string[] modInfosArr = gameModeModInfos.Select( m => m.Name ).ToArray();
			//string dataJson = JsonConvert.SerializeObject( modInfosArr, new JsonSerializerSettings() );
			string dataJson = $"[\n  \"{this.Name}\"";
			foreach( ModInfo modInfo in gameModeModInfos ) {
				dataJson += $",\n  \"{modInfo.Name}\"";
			}
			dataJson += $"\n]";

			File.WriteAllText( modListJsonFullPath, dataJson );
		}


		////////////////

		internal void BackupEnabledMods() {
			char div = Path.DirectorySeparatorChar;
			string modsFolder = Main.SavePath
				+ div
				+ "Mods";
			string modListJsonFullPath = modsFolder
				+ div
				+ "enabled.json";
			string modPacksFolder = modsFolder
				+ div
				+ "ModPacks";

			//

			byte[] buf = FileUtilities.ReadAllBytes( modListJsonFullPath, false );
			string enabledJson = System.Text.Encoding.UTF8.GetString( buf );

			//

			string backupPath = $"{modPacksFolder}{div}Pre AM Mods List Backup.json";

//this.Logger.Info( $"folder 1: \"{backupPath}\"" );
			for( int i=0; FileUtilities.Exists(backupPath, false); i++ ) {
				backupPath = $"{modPacksFolder}{div}Pre AM Mods List Backup ({i}).json";
			}

			//

			File.WriteAllText( backupPath, enabledJson );
		}
	}
}