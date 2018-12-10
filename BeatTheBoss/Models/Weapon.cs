using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    abstract class Weapon
    {
        public Rectangle spriteSource;
        public float damage;
        public Vector2 locationR;
        public Vector2 locationL;
        public float rotation;
        public bool attacking;
        public bool inattack;

        public abstract void Update(GameTime gameTime);
    }

}
