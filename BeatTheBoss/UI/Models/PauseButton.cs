﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Models
{
    class PauseButton : Components.Button
    {
        public PauseButton(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, string text, SpriteFont font, Color fontColor) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, text, font, fontColor)
        {

        }

        public override void OnClick()
        {
            base.OnClick();
            GameplayManager.self.CurrLevel.UIContainers.Push(new Containers.PauseContainer());
        }
    }
}
