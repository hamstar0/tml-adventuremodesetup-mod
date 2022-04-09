using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureModeSetup {
	public class AMSConfig : ModConfig {
		public static AMSConfig Instance => ModContent.GetInstance<AMSConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ClientSide;



		////////////////
		
		public bool ForceInstallPromptEachLoad { get; set; } = false;
	}
}
