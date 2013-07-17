using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using LimeWorksV2;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LimeWorksV2
{
    public class ScreenManager
    {
        /// <summary>
        /// Screen Manager private Instance
        /// </summary>
        private static ScreenManager instance;

        /// <summary>
        /// Screen Dimensions { private set, public get }
        /// </summary>
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }

        /// <summary>
        /// ScreenManager ContentManager { private set, public get }
        /// </summary>
        [XmlIgnore]
        public ContentManager Content { private set; get; }

        /// <summary>
        /// XML GameScreen Manager
        /// </summary>
        XMLManager<GameScreen> XMLGameScreenManager;

        /// <summary>
        /// Screen currently is use / Screen that we are transitioning to
        /// </summary>
        public GameScreen CurrentScreen, NewScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool isTransitioning { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XMLManager<ScreenManager> xml = new XMLManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }
                return instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            NewScreen = (GameScreen)Activator.CreateInstance(Type.GetType("LimeWorksV2." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            isTransitioning = true;
        }

        void Transition(GameTime gameTime)
        {
            if (isTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    CurrentScreen.UnloadContent();
                    CurrentScreen = NewScreen;
                    XMLGameScreenManager.Type = CurrentScreen.Type;
                    if (File.Exists(CurrentScreen.XmlPath))
                        CurrentScreen = XMLGameScreenManager.Load(CurrentScreen.XmlPath);
                    CurrentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    isTransitioning = false;

                }
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(1280, 720);
            CurrentScreen = new SplashScreen();
            XMLGameScreenManager = new XMLManager<GameScreen>();
            XMLGameScreenManager.Type = CurrentScreen.Type;
            CurrentScreen = XMLGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
            if (isTransitioning)
                Image.Draw(spriteBatch);
        }
    }
}
