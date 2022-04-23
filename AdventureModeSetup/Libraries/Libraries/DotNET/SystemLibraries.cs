using System;
using System.Diagnostics;


namespace AdventureModeSetup.Libraries.Libraries.DotNET {
	public class SystemLibraries {
		public static void OpenUrl( string url ) {
			try {
				Process.Start( url );
			} catch {
				try {
					//if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					//else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
					//else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
					url = url.Replace( "&", "^&" );
					Process.Start( new ProcessStartInfo( "cmd", "/c start "+url ) { CreateNoWindow = true } );
				} catch( Exception ) {
					try {
						Process.Start( "xdg-open", url );
					} catch( Exception ) {
						Process.Start( "open", url );
					}
				}
			}
		}
	}
}
