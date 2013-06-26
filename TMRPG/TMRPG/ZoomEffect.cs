using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LimeWorksV2
{
    public class ZoomEffect : ImageEffect
    {
        public float ZoomSpeed, ZoomMin, ZoomMax;
        public bool Increase;

        public ZoomEffect()
        {
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
