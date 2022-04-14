using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AdventureModeSetup {
	public partial class AMSMod : Mod {
		public static void PremultiplyTexture( Texture2D texture ) {
			Color[] buffer = new Color[texture.Width * texture.Height];

			texture.GetData( buffer );

			for( int i = 0; i < buffer.Length; i++ ) {
				buffer[i] = Color.FromNonPremultiplied( buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A );
			}

			texture.SetData( buffer );
		}

		////////////////

		private static void SafelyEndSpriteBatch_If() {
			var mostAccess = BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.Static;

			Type sbType = typeof( SpriteBatch );

			FieldInfo sbBegunField = sbType.GetField( "inBeginEndPair", mostAccess );
			if( sbBegunField == null ) {
				sbBegunField = sbType.GetField( "_beginCalled", mostAccess );
			}
			if( sbBegunField == null ) {
				sbBegunField = sbType.GetField( "beginCalled", mostAccess );
			}

			//

			if( (bool)sbBegunField.GetValue(Main.spriteBatch) ) {
				Main.spriteBatch.End();
			}
		}
	}
}