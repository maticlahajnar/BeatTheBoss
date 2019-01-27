using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    class WaterFountain
    {
        public Vector2 position;
        public float frameTimer;
        public float frameLimitTime;
        public int frame;

        public Rectangle[] spriteLocations;
        public Rectangle spriteSource;

        public WaterFountain(Vector2 position)
        {
            this.position = position;
            spriteSource = new Rectangle(532,891,65,101);
            spriteLocations = new Rectangle[] { new Rectangle(532, 891, 65, 101), new Rectangle(604, 891, 65, 101), new Rectangle(676, 891, 65, 101) };
            this.frameLimitTime = 300f;
        }

        public void Update(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer >= frameLimitTime)
            {
                frame++;
                if (frame >= spriteLocations.Length)
                    frame = 0;
                frameTimer = 0f;
                spriteSource = spriteLocations[frame];
            }
        }
    }
}
