using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private bool UnpackMod_If( string modName, bool forceUnpack ) {
			LoadStatus statusFlags = this.GetModStatusFlags( modName );

			if( (statusFlags & LoadStatus.MissingExternally) == 0 ) {
				return false;
			}

			//

			string fullFilePath = AMSMod.GetModFullFilePath( modName, out string modFileNameExt );

			if( File.Exists(fullFilePath) ) {
				if( !forceUnpack ) {
					return false;
				}

				File.Delete( fullFilePath );
			}

			//

			string internalFilePath = $"ModFiles/{modFileNameExt}";

			if( !this.FileExists(internalFilePath) ) {
				throw new Exception( $"Missing installable mod {modName}" );
			}

			//

			byte[] modFileData = this.GetFileBytes( internalFilePath );

			File.WriteAllBytes( fullFilePath, modFileData );

			if( !File.Exists(fullFilePath) ) {
				string err = $"Could not write mod file {modName}.";

				this.Logger.Error( err );
				throw new Exception( err );
			}

			//

			return true;
		}
	}
}