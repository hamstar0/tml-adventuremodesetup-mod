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
	}
}