using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Particles
{
    public class BloodParticleEngine
    {
        private Random random;
        private List<Particle> particles;
        private Rectangle[] textures;

        public BloodParticleEngine()
        {
            random = new Random();
            textures = TextureManager.bloodParticles;
            particles = new List<Particle>();
        }

        public void GenerateNewParticles(Vector2 position, int count)
        {
            for(int i = 0; i < count; i++)
            {
                Rectangle source = textures[random.Next(textures.Length)];
                Vector2 velocity = new Vector2(
                        1f * (float)(random.NextDouble() * 2 - 1),
                        1f * (float)(random.NextDouble() * 2 - 1));
                float angle = 0;
                float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
                float size = 1;
                int ttl = 20 + random.Next(40);
                particles.Add(new Particle(source, position, velocity, angle, angularVelocity, size, ttl));
            }
        }

        public void Update()
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
