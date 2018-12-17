using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Physics
{
    abstract class PollygonCollider : Collider
    {
        public Vector2[] Points;
        public Vector2 position;
        public Vector2 center;

        public abstract void ApplyCollision(PollygonCollider other);
        public abstract void ApplyCollision(BoxCollider other);

        public abstract Vector2 GetActualPointPosition(int index);
    }
}
