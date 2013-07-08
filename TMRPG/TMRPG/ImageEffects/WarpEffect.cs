using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LimeWorksV2
{
    public class WarpEffect : ImageEffect
    {
        public float ZoomSpeed, ZoomMin, ZoomMax, ZoomMinX, ZoomMaxX, ZoomMinY, ZoomMaxY;
        public bool IncreaseX, IncreaseY;

        public WarpEffect()
        {
            ZoomSpeed = 1f;
            ZoomMin = 0.5f;
            ZoomMax = 1.5f;
            ZoomMaxX = ZoomMaxY = 0.0f;
            IncreaseX = IncreaseY = false;
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
                if (!IncreaseX)
                    image.Scale.X -= ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.Scale.X += ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (image.Scale.X < ZoomMinX)
                {
                    IncreaseX = true;
                    image.Scale.X = ZoomMinX;
                }
                else if (image.Scale.X > ZoomMaxX)
                {
                    IncreaseX = false;
                    image.Scale.X = ZoomMaxX;
                }


                if (!IncreaseY)
                    image.Scale.Y -= ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.Scale.Y += ZoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (image.Scale.Y < ZoomMinY)
                {
                    IncreaseY = true;
                    image.Scale.Y = ZoomMinY;
                }
                else if (image.Scale.Y > ZoomMaxY)
                {
                    IncreaseY = false;
                    image.Scale.Y = ZoomMaxY;
                }
            }
        }
    }
}
