using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Particles
{
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle;
        public float AngularVelocity;
        public float Size;
        public int TTL;
        public Rectangle SpriteSource;

        public Particle(Rectangle spriteSource, Vector2 position, Vector2 velocity, float angle, float angularVelocity, float size, int ttl)
        {
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            SpriteSource = spriteSource;
            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(SpriteSource.Width / 2, SpriteSource.Height / 2);

            spriteBatch.Draw(TextureManager.spriteSheet, Position, SpriteSource, Color.White, Angle, origin, Size, SpriteEffects.None, 1f);
        }
    }
}
