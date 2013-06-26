using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace LimeWorksV2
{
    public class ShatterEffect : ImageEffect
    {
        public float ZoomSpeed, ZoomMin, ZoomMax;
        public bool Increase;

        Texture2D UpLeft, UpRight, DownLeft, DownRight;

        #region Create Part Image
        /// <summary>
        /// Creates a new image from an existing image.
        /// </summary>
        /// <param name="bounds">Area to use as the new image.</param>
        /// <param name="source">Source image used for getting a part image.</param>
        /// <returns>Texture2D.</returns>
        public static Texture2D CreatePartImage(Rectangle bounds, Texture2D source)
        {
            //Declare variables
            Texture2D result;
            Color[]
                sourceColors,
                resultColors;

            //Setup the result texture
            result = new Texture2D(ScreenManager.Instance.GraphicsDevice, bounds.Width, bounds.Height);

            //Setup the color arrays
            sourceColors = new Color[source.Height * source.Width];
            resultColors = new Color[bounds.Height * bounds.Width];

            //Get the source colors
            source.GetData<Color>(sourceColors);

            //Loop through colors on the y axis
            for (int y = bounds.Y; y < bounds.Height + bounds.Y; y++)
            {
                //Loop through colors on the x axis
                for (int x = bounds.X; x < bounds.Width + bounds.X; x++)
                {
                    //Get the current color
                    resultColors[x - bounds.X + (y - bounds.Y) * bounds.Width] =
                        sourceColors[x + y * source.Width];
                }
            }

            //Set the color data of the result image
            result.SetData<Color>(resultColors);

            //return the result
            return result;
        }
        #endregion



        public ShatterEffect()
        {
            UpLeft = CreatePartImage(new Rectangle(0, 0, image.Texture.Width / 2, image.Texture.Height / 2), image.Texture);

            ZoomSpeed = 1f;
            ZoomMin = 0.5f;
            ZoomMax = 1.5f;
            Increase = false;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActive)
            {
                if (!Increase)
                    image.Scale -= new Vector2(ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                else
                    image.Scale += new Vector2(ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if (image.Scale.X < ZoomMin)
                {
                    Increase = true;
                    image.Scale = new Vector2(ZoomMin);
                }
                else if (image.Scale.X > ZoomMax)
                {
                    Increase = false;
                    image.Scale = new Vector2(ZoomMax);
                }
            }
            else
                image.Scale = new Vector2(ZoomMax);
        }
    }
}
