using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileMapEditor
{
    public class Editor : GraphicsDeviceControl
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        Map map;
        int layerNumber;
        public List<Image> Selector;
        string[] selectorPath = { "TileMapEditor/Selector/TopLeft", "TileMapEditor/Selector/TopRight", "TileMapEditor/Selector/BottomLeft", "TileMapEditor/Selector/BottomRight" };

        public event EventHandler OnInitialize;

        public Editor()
        {
            map = new Map();
            layerNumber = 0;
            Selector = new List<Image>();

            for (int i = 0; i < 4; i++)
                Selector.Add(new Image());
        }

        public Layer CurrentLayer
        {
            get { return map.Layer[layerNumber]; }
        }

        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 4; i++)
            {
                Selector[i].Path = selectorPath[i];
                Selector[i].Initialize(content);
            }

            XmlSerializer xml = new XmlSerializer(map.GetType());
            Stream stream = File.Open("Load/Map1.xml", FileMode.Open);
            map = (Map)xml.Deserialize(stream);
            map.Initialize(content);

            if (OnInitialize != null)
                OnInitialize(this, null);
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
                map.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
