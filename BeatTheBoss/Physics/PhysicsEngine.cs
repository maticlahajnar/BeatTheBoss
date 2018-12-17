using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Physics
{
    class PhysicsEngine
    {
        public void CheckForCollision(Scenes.Level currLevel)
        {
            for(int i = 0; i < currLevel.items.Length; i++)
            {
                if (!(currLevel.items[i] is Collider))
                    continue;

                for(int j = 0; j < currLevel.items.Length; j++)
                {
                    if (i == j || !(currLevel.items[j] is Collider))
                        continue;

                    Collider.CheckForCollision(((Collider)currLevel.items[i]), ((Collider)currLevel.items[j]));
                }
            }
        }
    }
}
