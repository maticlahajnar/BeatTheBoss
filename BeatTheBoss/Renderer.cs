using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss
{
    class Renderer
    {
        private SpriteBatch spriteBatch;
        private Rectangle mainFrame;

        public Renderer(SpriteBatch spriteBatch, Rectangle mainFrame)
        {
            this.spriteBatch = spriteBatch;
            this.mainFrame = mainFrame;
        }

        public void Draw(Scenes.Level currLevel, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.background, mainFrame, Color.White);
            spriteBatch.Draw(TextureManager.spriteSheet, Vector2.Zero, TextureManager.KnightTexture, Color.White);
            
            foreach (object item in currLevel.items)
            {
                if (item is Models.Room)
                {
                    spriteBatch.Draw(((Models.Room)item).texture, mainFrame, Color.White);
                } else
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Models.Player)item).position, ((Models.Player)item).spriteSource, Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}
