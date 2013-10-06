using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LimeWorksV2
{
    public class GamePlayScreen : GameScreen
    {

        Player player;
        Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XMLManager<Player> playerLoader = new XMLManager<Player>();
            XMLManager<Map> mapLoader = new XMLManager<Map>();
            player = playerLoader.Load("Load/GamePlay/Player.xml");
            map = mapLoader.Load("Load/GamePlay/Maps/Map1.xml");

            player.LoadContent();
            map.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime, ref player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");
            player.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
