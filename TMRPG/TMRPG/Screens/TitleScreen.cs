using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LimeWorksV2
{
    public class TitleScreen : GameScreen
    {
        //public Image Image;
        public MenuManager menuManager;

        public TitleScreen()
        {
            menuManager = new MenuManager();        }

        public override void LoadContent()
        {
            base.LoadContent();
            //Image.LoadContent();
            menuManager.LoadContent("Load/Menus/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            //Image.UnloadContent();
            //menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Image.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Image.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
