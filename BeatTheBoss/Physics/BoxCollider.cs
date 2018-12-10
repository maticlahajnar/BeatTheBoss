using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Physics
{
    class BoxCollider
    {
        public int height;
        public int width;
        public bool trigger;

        public BoxCollider(int height, int width, bool isTrigger)
        {
            this.height = height;
            this.width = width;
            this.trigger = isTrigger;
        }

        public void CollideWith(BoxCollider other)
        {

        }
    }
}
