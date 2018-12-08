using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Scenes.Levels
{
    class BasicLevel : Level
    {

        public BasicLevel()
        {
            items = new Object[2];
            items[0] = new Models.Room(TextureManager.background);
            items[1] = new Models.Player(TextureManager.KnightTexture);
        }
    }
}
