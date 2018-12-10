using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Scenes
{
    abstract class Level
    {
        public Object[] items;

        public abstract void Update(GameTime gameTime);
        public abstract void Unload();

    }
}
