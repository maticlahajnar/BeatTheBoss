using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models
{
    class LadderTrigger : Physics.BoxCollider
    {
        private bool triggered = false;

        public LadderTrigger()
        {
            this.area = new Rectangle(1144, 180, 16, 12);
            isTrigger = true;
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (!triggered && other is Player)
            {
                GameplayManager.self.CurrLevel.UIContainers.Push(new UI.Containers.FadeOut());
                triggered = true;
            }
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            
        }
    }
}
