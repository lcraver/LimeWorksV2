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
    public class SplashScreen : GameScreen
    {
        public Image Image;
        public SoundManager soundManager;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            soundManager.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
            soundManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            soundManager.Update(gameTime);

            if (InputManager.Instance.keyPressed(Keys.Enter, Keys.Space))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
