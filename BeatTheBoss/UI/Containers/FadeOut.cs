using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BeatTheBoss.UI.Containers
{
    class FadeOut : Container
    {
        float colorModifier = 0.0f;

        public FadeOut()
        {
            boundingBox = new Rectangle(0, 0, 1280, 720);
            components = new List<Component>();
            color = Color.Black * 0.0f;
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
                components.Add(new Components.Label("Room " + (GameplayManager.self.currLevelNumber + 1), new Rectangle(0, 0, 1280, 720), this, Color.White, TextureManager.fontRegular30));
            }

            colorModifier += gameTime.ElapsedGameTime.Milliseconds / 500f;

            color = Color.Black * colorModifier;
            MediaPlayer.Volume = Math.Min(SoundManager.volume * (1-colorModifier), SoundManager.volume);

            if(colorModifier >= 1f)
            {
                GameplayManager.self.TryNextLevel();
            }
        }
    }
}
