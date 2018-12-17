using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models
{
    class ColliderOnlyObject : BoxCollider
    {
        public bool isTrigger;

        public ColliderOnlyObject(int X, int Y, int width, int height, bool istrigger)
        {
            this.area = new Rectangle(X, Y, width, height);
            this.isTrigger = istrigger;
        }

        public override void ApplyCollision(BoxCollider other)
        {
            
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            
        }
    }
}
