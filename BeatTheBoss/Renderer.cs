using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BeatTheBoss
{
    class Renderer
    {
        private SpriteBatch spriteBatch;
        private Rectangle mainFrame;
        public static GraphicsDevice graphicsDevice;

        private bool DRAW_COLLIDER = false;

        Texture2D t;

        public Renderer(SpriteBatch spriteBatch, Rectangle mainFrame, GraphicsDevice graphicsDeviceIn)
        {
            this.spriteBatch = spriteBatch;
            this.mainFrame = mainFrame;
            graphicsDevice = graphicsDeviceIn;

            t = new Texture2D(graphicsDevice,1, 1);
            t.SetData(new[] { Color.White });
        }

        public void Draw(GameTime gameTime)
        {
            Scenes.Level currLevel = GameplayManager.self.CurrLevel;

            spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(TextureManager.background, mainFrame, Color.White);
            spriteBatch.Draw(TextureManager.spriteSheet, Vector2.Zero, TextureManager.KnightTexture, Color.White);
            
            foreach (object item in currLevel.items)
            {
                if (item is Models.Room)
                {
                    spriteBatch.Draw(((Models.Room)item).texture, mainFrame, Color.White);
                }
                else if (item is Models.Player)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Models.Player)item).position, ((Models.Player)item).spriteSource, Color.White, 0f, new Vector2(0, 0), 1f, (((Models.Player)item).dir== -1) ? SpriteEffects.FlipHorizontally: SpriteEffects.None, 0.6f);

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
                else if(item is Models.Enemy)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Models.Enemy)item).position, ((Models.Enemy)item).spriteSource, ((Models.Enemy)item).color, 0f, new Vector2(0, 0), 1f, (((Models.Enemy)item).dir == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.6f);
                }

                if(DRAW_COLLIDER &&  item is Physics.BoxCollider)
                {
                    Rectangle box = ((Physics.BoxCollider)item).area;
                    int bw = 2;
                    spriteBatch.Draw(t, new Rectangle(box.Left, box.Top, bw, box.Height), Color.LightGreen); // Left
                    spriteBatch.Draw(t, new Rectangle(box.Right, box.Top, bw, box.Height), Color.LightGreen); // Right
                    spriteBatch.Draw(t, new Rectangle(box.Left, box.Top, box.Width, bw), Color.LightGreen); // Top
                    spriteBatch.Draw(t, new Rectangle(box.Left, box.Bottom, box.Width, bw), Color.LightGreen); // Bottom
                }
                else if (DRAW_COLLIDER && item is Physics.PollygonCollider)
                {
                    Physics.PollygonCollider collider = (Physics.PollygonCollider)item;
                    for(int i = 1; i < collider.Points.Length; i++)
                    {
                        DrawLine(Vector2.Add(collider.position, collider.Points[i - 1]), Vector2.Add(collider.position, collider.Points[i]));

                        if(i == collider.Points.Length - 1)
                            DrawLine(Vector2.Add(collider.position, collider.Points[i]), Vector2.Add(collider.position, collider.Points[0]));
                    }
                }
            }

            if(currLevel.UIContainers.Count > 0)
            {

                currLevel.UIContainers.Peek().Draw(spriteBatch);


            }

            spriteBatch.Draw(TextureManager.cursorTexture, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            spriteBatch.End();
        }

        public void DrawLine(Vector2 start, Vector2 end)
        {
            spriteBatch.Draw(t, start, null, Color.LightGreen,
                             (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                             new Vector2(0f, 0f),
                             new Vector2(Vector2.Distance(start, end), 1f),
                             SpriteEffects.None, 0f);
        }
    }
}
