using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Models
{
    class QuitButton : Components.Button
    {
        public QuitButton(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, string text, SpriteFont font, Color fontColor) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, text, font, fontColor)
        {
            
        }

        public override void OnClick()
        {
            System.Diagnostics.Debug.WriteLine("Overriden method on click");
            Game1.self.Exit();
        }
    }
}
