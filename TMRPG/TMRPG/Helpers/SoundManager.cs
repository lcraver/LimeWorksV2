using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace LimeWorksV2
{
    public class SoundManager
    {
        public string Path;
        public Song Song;
        public float Volume;
        public bool IsMusicPaused, Fading, isPlaying;

        [XmlIgnore]
        private MusicFadeEffect fadeEffect;
        [XmlIgnore]
        ContentManager content;

        public SoundManager()
        {
            Path = String.Empty;
            Volume = 1.0f;
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Song = content.Load<Song>(Path);

            if (isPlaying)
                MediaPlayer.Play(Song);

            MediaPlayer.Volume = Volume;
        }

        public void UnloadContent()
        {
            Song.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            if (Fading && !IsMusicPaused)
            {
                if (Song != null && MediaPlayer.State == MediaState.Playing)
                {
                    if (fadeEffect.Update(gameTime.ElapsedGameTime))
                    {
                        Fading = false;
                    }

                    MediaPlayer.Volume = fadeEffect.GetVolume();
                }
                else
                {
                    Fading = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void FadeSong(float targetVolume, TimeSpan duration)
        {
            fadeEffect = new MusicFadeEffect(MediaPlayer.Volume, targetVolume, duration);
            Fading = true;
        }

        #region MusicFadeEffect
        private struct MusicFadeEffect
        {
            public float SourceVolume;
            public float TargetVolume;

            private TimeSpan _time;
            private TimeSpan _duration;

            public MusicFadeEffect(float sourceVolume, float targetVolume, TimeSpan duration)
            {
                SourceVolume = sourceVolume;
                TargetVolume = targetVolume;
                _time = TimeSpan.Zero;
                _duration = duration;
            }

            public bool Update(TimeSpan time)
            {
                _time += time;

                if (_time >= _duration)
                {
                    _time = _duration;
                    return true;
                }

                return false;
            }

            public float GetVolume()
            {
                return MathHelper.Lerp(SourceVolume, TargetVolume, (float)_time.Ticks / _duration.Ticks);
            }
        }
        #endregion

        #region ISMusicPlaying
        private bool ISMusicPlaying()
        {
            if (MediaPlayer.State == MediaState.Playing)
                return true;
            else
                return false;
        }
        #endregion
    }
}
