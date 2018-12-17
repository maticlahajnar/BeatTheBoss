using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
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
            items = new Object[8];
            items[0] = new Models.Room(TextureManager.background);
            items[1] = new Models.Player(TextureManager.KnightTexture);
            items[2] = new Models.PollyColliderObject(new Vector2[] { new Vector2(0, 0), new Vector2(30, 0), new Vector2(45, 30), new Vector2(30, 60), new Vector2(0, 60), new Vector2(-15, 30) }, new Vector2(500, 400), (Models.Player)items[1], new Vector2(15,30));
            items[3] = new Models.ColliderOnlyObject(0, 0, 1280, 20, false);
            items[4] = new Models.ColliderOnlyObject(0, 640, 1280, 80, false);
            items[5] = new Models.ColliderOnlyObject(0, 0, 20, 720, false);
            items[6] = new Models.ColliderOnlyObject(1260, 0, 20, 720, false);
            items[7] = new Models.PollyColliderObject(new Vector2[] { new Vector2(0, 0), new Vector2(30, 0), new Vector2(45, 30), new Vector2(30, 60), new Vector2(0, 60), new Vector2(-15, 30) }, new Vector2(200, 200), (Models.Player)items[1], new Vector2(15, 30));


            MediaPlayer.Play(SoundManager.basicLevelSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            ((Models.Player)items[1]).Update(gameTime);
            ((Models.PollyColliderObject)items[2]).Update(gameTime);
            ((Models.PollyColliderObject)items[7]).Update(gameTime);
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
        }
    }
}
