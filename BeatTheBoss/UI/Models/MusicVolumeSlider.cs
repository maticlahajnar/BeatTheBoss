using BeatTheBoss.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Models
{
    class MusicVolumeSlider : Slider
    {
        public MusicVolumeSlider(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationImage, Rectangle spriteLocationButton, Color color, Container parentContainer, float initialValue) : base(boundingBox, texture, spriteLocationImage, spriteLocationButton, color, parentContainer, initialValue)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sliderButton.isClicked)
                SoundManager.ChangeVolume(value);
        }
    }
}
