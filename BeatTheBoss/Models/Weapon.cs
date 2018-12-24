using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    abstract class Weapon : Physics.PollygonCollider
    {
        public Rectangle spriteSource;
        public float damage;

        public Vector2 locationR;
        public Vector2 locationL;

        public Vector2 locationForRendererR;
        public Vector2 locationForRendererL;

        public float rotation;
        public bool attacking;
        public bool inattack;
        public Player playerInstance;

        public abstract void Update(GameTime gameTime);
        public abstract Vector2 getRenderPosition();
    }

}
