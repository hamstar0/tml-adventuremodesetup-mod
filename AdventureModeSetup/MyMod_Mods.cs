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
		public const string BackupFileBaseName = "PreAMModsListBackup";



		////////////////

		private void GetEachModStatus(
					IEnumerable<ModInfo> gameModeModEntries,
					out ISet<ModInfo> outdatedMods,
					out ISet<ModInfo> deactivatedMods,
					out ISet<ModInfo> nonexistentMods,
					out ISet<string> extraMods ) {
			outdatedMods = new HashSet<ModInfo>();
			deactivatedMods = new HashSet<ModInfo>();
			nonexistentMods = new HashSet<ModInfo>();
			extraMods = new HashSet<string>();

			//

			var config = AMSConfig.Instance;

			foreach( Mod mod in ModLoader.Mods ) {
				if( !gameModeModEntries.Any(mi => mi.Name == mod.Name) ) {
					switch( mod.Name ) {
					case "AdventureModeSetup":
					case "ModLoader":
						break;
					default:
						if( !config.NonAdventureModeModsAllowed.Contains(mod.Name) ) {
							extraMods.Add( mod.Name );
						}
						break;
					}
				}
			}

			//

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

				if( (statusFlags & LoadStatus.Outdated) == LoadStatus.Outdated ) {
					outdatedMods.Add( modInfo );
				}
				if( (statusFlags & LoadStatus.Deactivated) == LoadStatus.Deactivated ) {
					if( (modInfo.InfoFlags & ModInfo.ModTypeFlags.Optional) == 0 ) {
						deactivatedMods.Add( modInfo );
					}
				}
				if( (statusFlags & LoadStatus.MissingExternally) == LoadStatus.MissingExternally ) {
					nonexistentMods.Add( modInfo );
				}
			}
		}


		////////////////

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

			this.EnableModsInternally( toEnableMods );
		}

		private void EnableModsInternally( IEnumerable<ModInfo> gameModeModInfos ) {
			FieldInfo enabledModsFieldInfo = typeof( ModLoader )
				.GetField( "_enabledMods", BindingFlags.NonPublic | BindingFlags.Static );

			object rawInternalEnabledModsData = enabledModsFieldInfo.GetValue( null );
			HashSet<string> internalEnabledModsData = rawInternalEnabledModsData as HashSet<string>;

			//

			foreach( ModInfo modInfo in gameModeModInfos ) {
				internalEnabledModsData.Add( modInfo.Name );
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
				backupPath = $"{modPacksFolder}{div}{AMSMod.BackupFileBaseName} ({i}).json";
			}

			//

			File.WriteAllText( backupPath, enabledJson );
		}
	}
}