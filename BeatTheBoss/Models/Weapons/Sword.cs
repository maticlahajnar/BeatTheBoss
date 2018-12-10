using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models.Weapons
{
    class Sword : Weapon
    {
        public Sword()
        {
            this.locationR = new Vector2(60, 70);
            this.locationL = new Vector2(-20, 70);
            this.damage = 10f;
            this.spriteSource = new Rectangle(1171, 326, 22, 115);
            this.rotation = 0f;
            this.attacking = false;
        }

        public override void Update(GameTime gameTime)
        {
            if(attacking == true)
            {
                rotation -= 0.2f;
                if(rotation < -2f)
                {
                    attacking = false;
                }
            } else
            {
                if(rotation < 0)
                {
                    rotation += 0.2f;
                } else
                {
                    inattack = false;
                }
            }
        }
    }
}
