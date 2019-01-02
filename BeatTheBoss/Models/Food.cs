using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    class Food : Physics.BoxCollider
    {
        public float value;
        public Rectangle spriteSource;
        public Vector2 position;
        public Texture2D texture;

        public Food(Vector2 position, int value)
        {
            this.area = new Rectangle((int)position.X, (int)position.Y,32,42);
            this.position = position;
            this.spriteSource = new Rectangle(305, 5, 337, 47);
            this.value = value;
            this.texture = TextureManager.uiSpriteSheet;

        }

        public override void ApplyCollision(BoxCollider other)
        {
            // Food itself doesnt do anything on collision, only player picks it up
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            // Food itself doesnt do anything on collision, only player picks it up
        }
    }
}
