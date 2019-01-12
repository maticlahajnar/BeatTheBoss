using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Models
{
    class PlayerNameInput : Components.InputField
    {
        public PlayerNameInput(Rectangle boundingBox, Container parentContainer) : base(boundingBox, TextureManager.uiSpriteSheet, new Rectangle(4, 141, 206, 49), new Rectangle(4, 141, 206, 49), Color.White, parentContainer, SettingsManager.playerName, TextureManager.fontRegular12, Color.Black)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!this.isActive)
                SettingsManager.playerName = this.textBuilder.ToString();
        }
    }
}
