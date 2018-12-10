using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss
{
    class Renderer
    {
        private SpriteBatch spriteBatch;
        private Rectangle mainFrame;
        private SpriteFont font;

        public Renderer(SpriteBatch spriteBatch, Rectangle mainFrame, SpriteFont font)
        {
            this.spriteBatch = spriteBatch;
            this.mainFrame = mainFrame;
            this.font = font;
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
                } else if (item is Models.Player)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Models.Player)item).position, ((Models.Player)item).spriteSource, Color.White, 0f, new Vector2(0, 0), 1f, (((Models.Player)item).dir== -1) ? SpriteEffects.FlipHorizontally: SpriteEffects.None, 0.6f);
                    spriteBatch.DrawString(font, "Coordinates X:" + ((Models.Player)item).position.X + " Y:" + ((Models.Player)item).position.Y, new Vector2(20, 3), Color.Red);

                    if(((Models.Player)item).weapon != null)
                    {
                        spriteBatch.Draw(TextureManager.spriteSheet,
                            Vector2.Add((((Models.Player)item).dir == -1) ? ((Models.Player)item).weapon.locationL : ((Models.Player)item).weapon.locationR,
                            ((Models.Player)item).position), ((Models.Player)item).weapon.spriteSource, Color.White,
                            (((Models.Player)item).dir == -1) ? ((Models.Player)item).weapon.rotation : -((Models.Player)item).weapon.rotation,
                            new Vector2(0, ((Models.Player)item).weapon.spriteSource.Height),
                            1f,
                            (((Models.Player)item).dir == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                            0.5f);
                    }
                }
            }

            spriteBatch.End();
        }
    }
}
