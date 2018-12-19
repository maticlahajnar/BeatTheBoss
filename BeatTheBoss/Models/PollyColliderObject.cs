using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models
{
    class PollyColliderObject : Physics.PollygonCollider
    {
        public Vector2 direction;
        public float speed;
        public Player playerInstance;

        public PollyColliderObject(Vector2[] points, Vector2 position, Player player, Vector2 center)
        {
            this.Points = points;
            this.position = position;
            this.speed = 0.15f;
            this.playerInstance = player;
            this.center = Vector2.Add(position, center);
            this.direction = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Multiply(Vector2.Multiply(this.direction, this.speed), gameTime.ElapsedGameTime.Milliseconds);

            this.position = Vector2.Add(movement, position);

            center = Vector2.Add(center, movement);

            this.direction = Vector2.Normalize(Vector2.Subtract(playerInstance.area.Center.ToVector2(), this.center));

            if (float.IsNaN(this.direction.X))
                this.direction.X = 0;

            if (float.IsNaN(this.direction.Y))
                this.direction.Y = 0;
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.center, this.center));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
        }

        public override void ApplyCollision(BoxCollider other)
        {
            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.area.Center.ToVector2(), this.center));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
        }

        public override Vector2 GetActualPointPosition(int index)
        {
            return Vector2.Add(position, Points[index]);
        }
    }
}
