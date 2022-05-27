using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		internal void UnpackMods_Current() {
			foreach( ModInfo modInfo in ModInfo.AdventureModeMods ) {
				bool forceUnpack = this.OutdatedMods.Contains( modInfo );

				this.UnpackMod_If( modInfo.Name, forceUnpack );
			}
		}


		////////////////

		internal void EnableMods_Current() {
			char div = Path.DirectorySeparatorChar;
			string modsFolder = Main.SavePath
				+ div
				+ "Mods";
			string modListJsonFullPath = modsFolder
				+ div
				+ "enabled.json";

			//

			Directory.CreateDirectory( modsFolder );

			//

			//string[] modInfosArr = gameModeModInfos.Select( m => m.Name ).ToArray();
			//string dataJson = JsonConvert.SerializeObject( modInfosArr, new JsonSerializerSettings() );
			string dataJson = $"[\n  \"{this.Name}\"";
			foreach( ModInfo modInfo in ModInfo.AdventureModeMods ) {
				dataJson += $",\n  \"{modInfo.Name}\"";
			}
			dataJson += $"\n]\n";

			File.WriteAllText( modListJsonFullPath, dataJson );

			//

			var config = AMSConfig.Instance;

			IEnumerable<ModInfo> toEnableMods = ModInfo.AdventureModeMods
				.Where( mi => !config.SkipLoadingMods.Contains(mi.Name) );

			//

			this.ExclusivelyEnableModsInternally( toEnableMods );
		}

		private void ExclusivelyEnableModsInternally( IEnumerable<ModInfo> gameModeModInfos ) {
			FieldInfo enabledModsFieldInfo = typeof( ModLoader )
				.GetField( "_enabledMods", BindingFlags.NonPublic | BindingFlags.Static );

			object rawInternalEnabledModsData = enabledModsFieldInfo.GetValue( null );
			HashSet<string> internalEnabledModsData = rawInternalEnabledModsData as HashSet<string>;

			// Enable approved moved
			foreach( ModInfo modInfo in gameModeModInfos ) {
				internalEnabledModsData.Add( modInfo.Name );
			}

			// Disable unapproved mods
			foreach( string existingEnabledMod in internalEnabledModsData.ToArray() ) {
				if( existingEnabledMod == AMSMod.Instance.Name ) {
					continue;
				}

				if( !gameModeModInfos.Any(mi => mi.Name == existingEnabledMod) ) {
					internalEnabledModsData.Remove( existingEnabledMod );
				}
			}
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

			Directory.CreateDirectory( modPacksFolder );

			//

			byte[] buf = FileUtilities.ReadAllBytes( modListJsonFullPath, false );
			string enabledJson = System.Text.Encoding.UTF8.GetString( buf );

			//

			string backupPath = $"{modPacksFolder}{div}{AMSMod.BackupFileBaseName}.json";

//this.Logger.Info( $"folder 1: \"{backupPath}\"" );
			for( int i=0; FileUtilities.Exists(backupPath, false); i++ ) {
				backupPath = $"{modPacksFolder}{div}{AMSMod.BackupFileBaseName}{i}.json";
			}

			//

			File.WriteAllText( backupPath, enabledJson );
		}
	}
}