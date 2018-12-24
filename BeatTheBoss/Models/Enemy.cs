using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    abstract class Enemy : Physics.BoxCollider
    {
        public Rectangle spriteSource;
        public Vector2 position;
        public Vector2 direction;
        public float speed;
        public int dir;
        public Color color;
        public float hp;

        public abstract void Update(GameTime gameTime);
        public abstract void TakeDamage(float damage);
    }
}
