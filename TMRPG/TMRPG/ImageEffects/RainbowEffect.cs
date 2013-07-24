using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LimeWorksV2
{
    public class RainbowEffect : ImageEffect
    {
        public int ChangeSpeed;
        public string Colors;
        string[] colorsArray;
        public bool Increase;
        int currentColor;

        public RainbowEffect()
        {
            //color is being read from the xml file
            Colors = String.Empty;
            colorsArray = new string[7] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
            ChangeSpeed = 50;
            Increase = false;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);

            if (Colors != "")
                colorsArray = Colors.Split(':');
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
                //Add in way to change the time between color changes
                if (GetRandInt() == 1)
                    currentColor++;
                if (currentColor == colorsArray.Length)
                    currentColor = 0;
                image.imageColor = image.ParseColor(colorsArray[currentColor].ToString());
            }
            else
                image.imageColor = image.ParseColor(colorsArray[0].ToString());
        }

        private int GetRandInt()
        {
            return new Random().Next(1, ChangeSpeed);
        }
    }
}
