using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Physics
{
    abstract class BoxCollider : Collider
    {
        public Rectangle area;

        public abstract void ApplyCollision(BoxCollider other);
        public abstract void ApplyCollision(PollygonCollider other);
    }
}
