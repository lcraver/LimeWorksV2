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
        public int RandomMax;
        public string MoveType;
        public bool Increase;
        public Vector2 position;

        public JitterEffect()
        {
            MoveSpeed = 50f;
            MoveMin = 0f;
            MoveMax = 100f;
            MoveType = "Vertical";
            Increase = false;
            position = Vector2.Zero;
            RandomMax = 9;
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
                switch(MoveType)
                {
                    case "Vertical":
                        if (!Increase)
                            image.Position.Y += GetMoveSpeed(gameTime);
                        else
                            image.Position.Y -= GetMoveSpeed(gameTime);

                        if (image.Position.Y < MoveMin)
                        {
                            Increase = !Increase;
                            image.Position.Y = MoveMin;
                        }
                        else if (image.Position.Y > MoveMax)
                        {
                            Increase = !Increase;
                            image.Position.Y = MoveMax;
                        }
                        break;
                    case "Horizontal":
                        if (!Increase)
                            image.Position.X += GetMoveSpeed(gameTime);
                        else
                            image.Position.X -= GetMoveSpeed(gameTime);

                        if (image.Position.X < MoveMin)
                        {
                            Increase = !Increase;
                            image.Position.X = MoveMin;
                        }
                        else if (image.Position.X > MoveMax)
                        {
                            Increase = !Increase;
                            image.Position.X = MoveMax;
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
