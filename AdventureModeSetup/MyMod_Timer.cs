using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		private int TimerDuration = 0;
		private Action TimerAction = null;



		////////////////
		
		private void LoadTimer() {
			Main.OnTick += AMSMod._UpdateTimer;
		}
		
		private void UnloadTimer() {
			Main.OnTick -= AMSMod._UpdateTimer;
		}


		////////////////

		public bool IsTimerRunning() {
			return this.TimerAction != null;
		}

		////

		public void RunAfterTimer( int ticks, Action action ) {
			this.TimerDuration = ticks;
			this.TimerAction = action;
		}


		////////////////

		private void UpdateTimer() {
			if( this.TimerDuration > 0 ) {
				this.TimerDuration--;

				return;
			}

			//

			if( this.TimerAction != null ) {
				this.TimerAction.Invoke();

				this.TimerAction = null;
			}
		}

		////

		private static void _UpdateTimer() {
			AMSMod.Instance?.UpdateTimer();
		}
	}
}