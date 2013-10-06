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
    public class LoadGameScreen : GameScreen
    {
        public Image Image;
        public MenuManager menuManager;

        public LoadGameScreen()
        {
            menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            menuManager.LoadContent("Load/Menus/LoadGameMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
