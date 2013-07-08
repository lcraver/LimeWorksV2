using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LimeWorksV2
{
    public class FadeEffect : ImageEffect
    {
        public float FadeSpeed;
        public bool Increase;

        public FadeEffect()
        {
            FadeSpeed = 1;
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
                    image.Alpha -= GetFadeSpeed(gameTime);
                else
                    image.Alpha += GetFadeSpeed(gameTime);

                if (image.Alpha < 0.0f)
                {
                    Increase = !Increase;
                    image.Alpha = 0.0f;
                }
                else if (image.Alpha > 1.0f)
                {
                    Increase = !Increase;
                    image.Alpha = 1.0f;
                }
            }
            else
                image.Alpha = 1.0f;
        }

        private float GetFadeSpeed(GameTime gameTime)
        {
            return FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
