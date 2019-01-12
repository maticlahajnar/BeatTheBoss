using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Models
{
    class StartGame: Components.Button
    {
        public StartGame(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, SpriteFont font, Color fontColor) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, (SettingsManager.isGameSaved) ? "Continue game" : "New game" , font, fontColor)
        {

        }

        public override void OnClick()
        {
            GameplayManager.self.StartGame();
        }
    }
}
