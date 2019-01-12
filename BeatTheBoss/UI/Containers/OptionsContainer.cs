using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Containers
{
    class OptionsContainer : Container
    {
        public OptionsContainer()
        {
            boundingBox = new Rectangle(394, 75, 492, 570);
            components = new List<Component>();
            color = new Color(58, 64, 73) * 0.8f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.pixel, boundingBox, color);
            foreach (UI.Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (components.Count == 0)
            {
                components.Add(new Components.Label("Options", new Rectangle(0, 0, 492, 100), this, Color.White, TextureManager.fontBold30));

                components.Add(new Components.Label("Player name", new Rectangle(0, 100, 492, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Models.PlayerNameInput(new Rectangle(0, 120, 492, 60), this));

                components.Add(new Components.Label("Music", new Rectangle(0, 190, 492, 20), this, Color.White, TextureManager.fontRegular12));
                string text = (SoundManager.isMuted) ? "Unmute music" : "Mute music"; 
                components.Add(new Models.MuteMusicButton(new Rectangle(0, 210, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 4, 206, 68), Color.White, this, text, TextureManager.fontRegular12, Color.Black));

                components.Add(new Components.Label("Music volume", new Rectangle(0, 300, 492, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Models.MusicVolumeSlider(new Rectangle(139, 325, 214, 36), TextureManager.uiSpriteSheet, new Rectangle(4, 196, 214, 36), new Rectangle(229, 194, 10, 38), Color.White, this, SoundManager.volume));

                components.Add(new Models.BackButton(new Rectangle(0, 465, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Back", TextureManager.fontRegular12, Color.Black));
            }

            foreach (Component component in components)
                component.Update(gameTime);

        }
    }
}
