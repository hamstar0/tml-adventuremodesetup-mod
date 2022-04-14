using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public static AMSMod Instance;




		////////////////

		private UIInstallPromptDialog InstallPromptUI;

		////

		private ISet<ModInfo> OutdatedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> DeactivatedMods = new HashSet<ModInfo>();
		private ISet<ModInfo> MissingMods = new HashSet<ModInfo>();
		private ISet<string> ExtraMods = new HashSet<string>();

		////

		private AdventureModeLogo Logo;

		////

		private bool HasPostAddRecipes = false;
		private bool HasPostSetupContent = false;
		private bool HasAddRecipeGroups = false;



		////////////////

		public override void Load() {
			AMSMod.Instance = this;

			//

			this.LoadTimer();

			//
			
			if( Main.netMode == NetmodeID.Server || Main.dedServ ) {
				return;
			}

			//

			this.Logo = new AdventureModeLogo();

			//
			
			this.InstallPromptUI = new UIInstallPromptDialog();

			//

			this.GetEachModStatus(
				gameModeModEntries: ModInfo.AdventureModeMods,
				outdatedMods: out this.OutdatedMods,
				deactivatedMods: out this.DeactivatedMods,
				nonexistentMods: out this.MissingMods,
				extraMods: out this.ExtraMods
			);

			//
			
			On.Terraria.Main.DrawMenu += this.Main_DrawMenu_Inject;

			//

			this.HasAddRecipeGroups = false;
			this.HasPostAddRecipes = false;
			this.HasPostSetupContent = false;
		}


		public override void Unload() {
			this.Logo.Unload();

			//

			this.UnloadTimer();

			//

			this.Logo = null;

			AMSMod.Instance = null;
		}


		////////////////

		public override void PostAddRecipes() {
			this.HasPostAddRecipes = true;
		}

		public override void PostSetupContent() {
			this.HasPostSetupContent = true;
		}

		public override void AddRecipeGroups() {
			this.HasAddRecipeGroups = true;
		}


		////////////////

		public void ClearRecordedModStates() {
			this.OutdatedMods.Clear();
			this.MissingMods.Clear();
			this.DeactivatedMods.Clear();
			this.ExtraMods.Clear();
		}
	}
}