using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LimeWorksV2
{
    public class Menu
    {
        ContentManager content;

        public event EventHandler OnMenuChange;

        public string Axis, Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;

        public Texture2D particleImg;
        ParticleSystem particleSystem;
        int itemNumber;
        string id;

        public int ItemNumber
        {
            get { return itemNumber; }
        }

        public string ID
        {
            get { return id; }
            set 
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public Menu()
        {
            id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                AlignMenuItems();
                foreach (string s in split)
                    item.Image.ActivateEffect(s);
            }

            particleImg = this.content.Load<Texture2D>("ParticleBase1");
            particleSystem = new ParticleSystem(new Vector2(Items[itemNumber].Image.Position.X + Items[itemNumber].Image.SourceRect.Center.X, Items[itemNumber].Image.Position.Y + Items[itemNumber].Image.SourceRect.Center.Y));
            particleSystem.AddEmitter(new Vector2(0.01f, 0.015f),
                                        new Vector2(0, -1), new Vector2(1f * MathHelper.Pi, 1f * -MathHelper.Pi),
                                        new Vector2(0.5f, 0.75f),
                                        new Vector2(120, 140), new Vector2(60, 70f),
                                        Color.Orange, Color.Crimson, new Color(255, 165, 0, 0), new Color(255, 165, 0, 0),
                                        new Vector2(400, 500), new Vector2(100, 120), 1000, Vector2.Zero, particleImg);
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Effects.Contains("Particles"))
            {
                particleSystem.Position = new Vector2(Items[itemNumber].Image.Position.X + Items[itemNumber].Image.SourceRect.Center.X, Items[itemNumber].Image.Position.Y + Items[itemNumber].Image.SourceRect.Center.Y);
                particleSystem.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }

            if (Axis == "X")
            {
                if (InputManager.Instance.keyPressed(Keys.Right, Keys.D))
                    itemNumber++;
                else if (InputManager.Instance.keyPressed(Keys.Left, Keys.A))
                    itemNumber--;
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.keyPressed(Keys.Down, Keys.S))
                    itemNumber++;
                else if (InputManager.Instance.keyPressed(Keys.Up, Keys.W))
                    itemNumber--;
            }

            if (itemNumber < 0)
                itemNumber = Items.Count - 1;
            else if (itemNumber > Items.Count - 1)
                itemNumber = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].Image.IsActive = true;
                else
                    Items[i].Image.IsActive = false;

                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Effects.Contains("Particles"))
            {
                particleSystem.Draw(spriteBatch, 1, Vector2.Zero);
            }

            foreach (MenuItem item in Items)
                item.Image.Draw(spriteBatch);
        }

        #region Helpers

        public void Transition(float alpha)
        {
            foreach (MenuItem item in Items)
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                if (alpha == 0.0f)
                    item.Image.FadeEffect.Increase = true;
                else
                    item.Image.FadeEffect.Increase = false;
            }
        }

        public void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (MenuItem item in Items)
                dimensions += new Vector2(item.Image.SourceRect.Width,
                    item.Image.SourceRect.Height);

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                    item.Image.Position = new Vector2(dimensions.X,
                        (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2);
                else if (Axis == "Y")
                    item.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X -
                        item.Image.SourceRect.Width) / 2, dimensions.Y);
                
                dimensions += new Vector2(item.Image.SourceRect.Width,
                    item.Image.SourceRect.Height);
            }
        }

        #endregion
    }
}
