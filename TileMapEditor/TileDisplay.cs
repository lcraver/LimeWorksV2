using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileMapEditor
{
    public class TileDisplay : GraphicsDeviceControl
    {

        Editor editor;
        Image image;
        SpriteBatch spriteBatch;
        List<Image> selector;
        float selectorScale;
        bool isMouseDown;
        Vector2 mousePosition, clickPosition;

        public TileDisplay(ref Editor editor)
        {
            this.editor = editor;
            editor.OnInitialize += LoadContent;
        }

        void LoadContent(object sender, EventArgs e)
        {
            image = editor.CurrentLayer.Image;
            selector = editor.Selector;
            selectorScale = selector[0].SourceRect.Width / editor.CurrentLayer.TileDimensions.X;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MouseDown += TileDisplay_MouseDown;
            MouseUp += delegate { isMouseDown = false; };
            MouseMove += TileDisplay_MouseMove;
        }

        void TileDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
            {
                clickPosition = mousePosition;

                foreach (Image img in selector)
                    img.Position = mousePosition;
            }

            isMouseDown = true;
        }

        void TileDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Vector2((int)(e.X / (editor.CurrentLayer.TileDimensions.X * selectorScale)),
                (int)(e.Y / (editor.CurrentLayer.TileDimensions.Y * selectorScale)));

            mousePosition *= (editor.CurrentLayer.TileDimensions.X * selectorScale);

            if (mousePosition != clickPosition && isMouseDown)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i % 2 == 0 && mousePosition.X < clickPosition.X)
                        selector[i].Position.X = mousePosition.X;
                    else if (i % 2 != 0 && mousePosition.X > clickPosition.X)
                        selector[i].Position.X = mousePosition.X;
                    
                    if (i < 2 && mousePosition.Y < clickPosition.Y)
                        selector[i].Position.Y = mousePosition.Y;
                    else if (i >= 2 && mousePosition.Y > clickPosition.Y)
                        selector[i].Position.Y = mousePosition.Y;
                }
            }
            else
            {
                foreach (Image img in selector)
                    img.Position = mousePosition;
            }

            Invalidate();
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
                image.Scale = selectorScale;
                image.Draw(spriteBatch);
                foreach (Image img in selector)
                {
                    img.Draw(spriteBatch);
                }
            spriteBatch.End();
        }
    }
}
