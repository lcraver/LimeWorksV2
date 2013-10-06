using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileMapEditor
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer;
        public Vector2 TileDimensions;

        public void Initialize(ContentManager content)
        {
            foreach (Layer l in Layer)
                l.Initialize(content, TileDimensions);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer l in Layer)
                l.Draw(spriteBatch);
        }
    }
}
