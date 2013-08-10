using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LimeWorksV2
{
    public class JitterEffect : ImageEffect
    {
        public float MoveSpeed, MoveMin, MoveMax;
        /// <summary>
        /// Maximum amount of random numbers (higher number : longer time to change / lower number : shorter time to change)
        /// </summary>
        public int RandomMax;

        /// <summary>
        /// What type of movement is the Image doing ( Vertical / Horizontal / Random )
        /// </summary>
        public string MoveType;

        /// <summary>
        /// Whether or not the effect is Increasing. 
        /// </summary>
        public bool Increase;

        /// <summary>
        /// Origional Position of Image (flawed)
        /// </summary>
        private Vector2 origPosition;

        public JitterEffect()
        {
            MoveSpeed = 300f;
            MoveMin = -100f;
            MoveMax = 100f;
            MoveType = "Horizontal";
            Increase = false;
            RandomMax = 9;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
            origPosition = Image.Position;
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
                switch (MoveType)
                {
                    case "Vertical":
                        if (!Increase)
                            image.Position.Y += GetMoveSpeed(gameTime);
                        else
                            image.Position.Y -= GetMoveSpeed(gameTime);

                        if (image.Position.Y < MoveMin + origPosition.Y)
                        {
                            Increase = !Increase;
                            image.Position.Y = MoveMin + origPosition.Y;
                        }
                        else if (image.Position.Y > MoveMax + origPosition.Y)
                        {
                            Increase = !Increase;
                            image.Position.Y = MoveMax + origPosition.Y;
                        }
                        break;
                    case "Horizontal":
                        if (!Increase)
                            image.Position.X += GetMoveSpeed(gameTime);
                        else
                            image.Position.X -= GetMoveSpeed(gameTime);

                        if (image.Position.X < MoveMin + origPosition.X)
                        {
                            Increase = !Increase;
                            image.Position.X = MoveMin + origPosition.X;
                        }
                        else if (image.Position.X > MoveMax + origPosition.X)
                        {
                            Increase = !Increase;
                            image.Position.X = MoveMax + origPosition.X;
                        }
                        break;
                    case "Random":
                        if (!Increase)
                            if (GetRandBool())
                                image.Position.X += GetMoveSpeed(gameTime);
                            else
                                image.Position.Y += GetMoveSpeed(gameTime);
                        else
                            if (GetRandBool())
                                image.Position.X -= GetMoveSpeed(gameTime);
                            else
                                image.Position.Y -= GetMoveSpeed(gameTime);
                        if (GetRandInt() == 1)
                        {
                            Increase = !Increase;
                        }
                        break;
                }
            }
            else
            {
                switch (MoveType)
                {
                    case "Vertical":
                        image.Position.Y = origPosition.Y;
                        break;

                    case "Horizontal":
                        image.Position.X = origPosition.X;
                        break;

                    case "Random":
                        image.Position = origPosition;
                        break;
                }
            }
        }
        
        private bool GetRandBool()
        {
            return new Random().Next(100) % 2 == 0;
        }

        private int GetRandInt()
        {
            return new Random().Next(1, RandomMax);
        }

        private float GetMoveSpeed(GameTime gameTime)
        {
            return MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
