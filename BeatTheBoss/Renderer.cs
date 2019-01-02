using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BeatTheBoss.Models;

namespace BeatTheBoss
{
    class Renderer
    {
        private SpriteBatch spriteBatch;
        private Rectangle mainFrame;
        public static GraphicsDevice graphicsDevice;

        private bool DRAW_COLLIDER = true;

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
            
            foreach (object item in currLevel.items)
            {
                if (item is Room)
                {
                    spriteBatch.Draw(((Room)item).texture, mainFrame, Color.White);
                }
                else if (item is Player)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Player)item).position, ((Player)item).spriteSource, Color.White, 0f, new Vector2(0, 0), 1f, (((Player)item).dir== -1) ? SpriteEffects.FlipHorizontally: SpriteEffects.None, 0.6f);
                }
                else if(item is Enemy)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet, ((Enemy)item).position, ((Enemy)item).spriteSource, ((Enemy)item).color, 0f, new Vector2(0, 0), 1f, (((Enemy)item).dir == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.6f);
                } else if(item is Weapon)
                {
                    spriteBatch.Draw(TextureManager.spriteSheet,
                        ((Weapon)item).getRenderPosition(),
                        ((Weapon)item).spriteSource,
                        Color.White,
                        (((Weapon)item).playerInstance.dir == 1) ? -((Weapon)item).rotation : ((Weapon)item).rotation,
                        new Vector2(0, ((Weapon)item).spriteSource.Height),
                        1f,
                        ((Weapon)item).playerInstance.dir == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                        0.6f);
                } else if(item is Food)
                {
                    spriteBatch.Draw(((Food)item).texture, ((Food)item).position, ((Food)item).spriteSource, Color.White);
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
                        DrawLine(collider.GetActualPointPosition(i-1), collider.GetActualPointPosition(i));

                        if(i == collider.Points.Length - 1)
                            DrawLine(collider.GetActualPointPosition(i), collider.GetActualPointPosition(0));
                    }
                }
            }

            // Draw blood particles
            Game1.self.bloodParticleEngine.Draw(spriteBatch);

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
