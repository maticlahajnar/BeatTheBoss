using BeatTheBoss.UI.Containers;
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
        int type = -1;
        public StartGame(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, SpriteFont font, Color fontColor, int type) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, (type == 0) ? "Play" : (type == 1) ? "New game" : "Continue", font, fontColor)
        {
            this.type = type;
        }

        public override void OnClick()
        {
            if (type == 0)
            {
                if (SettingsManager.isGameSaved)
                    GameplayManager.self.CurrLevel.UIContainers.Push(new NewOrContinue());
                else
                    GameplayManager.self.StartGame(true);
            }
            else if (type == 1)
            {
                GameplayManager.self.StartGame(true);
            }
            else if (type == 2)
            {
                GameplayManager.self.StartGame(false);
            }
        }
    }
}
