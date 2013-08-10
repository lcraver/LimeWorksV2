using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LimeWorksV2
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public bool IsActive;

        /// <summary>
        /// Public Strings to set the colors of images and text
        /// </summary>
        public string TextColor, ImageColor;

        public Color textColor, imageColor, drawColor;

        public Texture2D Texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<string, ImageEffect> effectList;
        public string Effects;

        public FadeEffect FadeEffect;
        public ZoomEffect ZoomEffect;
        public JitterEffect JitterEffect;
        public RainbowEffect RainbowEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectList.Add(effect.GetType().ToString().Replace("LimeWorksV2.",""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }

            if(Effects != String.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);

            string[] split = Effects.Split(':');
            foreach (string s in split)
                ActivateEffect(s);
        }

        public Color ParseColor (string color)
        {
            string colorName = color;
            if (colorName.Contains("Random"))
            {
                Random r = new Random();
                return new Color(
                        ( byte ) r.Next ( 0, 255 ),
                        ( byte ) r.Next ( 0, 255 ),
                        ( byte ) r.Next ( 0, 255 )
                    );
            }
            else
            {
                if (typeof(Color).GetProperty(colorName) != null)
                {
                    PropertyInfo colorProperty = typeof(Color).GetProperty(colorName);
                    return (Color)colorProperty.GetValue(null, null);
                }
                else
                {
                    //To Alert that the inputed color isn't real
                    return Color.BlueViolet;
                }
            }
        }

        public Image()
        {
            Path = Text = Effects = TextColor = ImageColor = String.Empty;
            FontName = "Fonts/kilix";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
            imageColor = Color.White;
            textColor = Color.Black;
            drawColor = Color.White;
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (TextColor != "")
                textColor = ParseColor(TextColor);

            if (ImageColor != "")
                imageColor = ParseColor(ImageColor);

            if (Path != String.Empty)
            {
                string pathtest = content.RootDirectory + "/" + Path + ".xnb";
                if (File.Exists(pathtest))
                    Texture = content.Load<Texture2D>(Path);
                else
                    Texture = content.Load<Texture2D>("Debuging/null");
            }

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if(Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if(Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, imageColor);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, textColor);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);


            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<ZoomEffect>(ref ZoomEffect);
            SetEffect<JitterEffect>(ref JitterEffect);
            SetEffect<RainbowEffect>(ref RainbowEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2,
                SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position + origin, SourceRect, drawColor * Alpha,
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
