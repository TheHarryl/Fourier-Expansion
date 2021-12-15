using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public static class InternalManager
    {
        private static GraphicsDeviceManager _graphics;
        private static Dictionary<int, GameState> _states = new Dictionary<int, GameState>();

        public static ContentManager Content;

        public static Dictionary<string, Texture2D> LoadedTextures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> LoadedFonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, SoundEffect> LoadedSoundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> LoadedSongs = new Dictionary<string, Song>();

        public static void Initialize(GraphicsDeviceManager graphics, ContentManager content)
        {
            _graphics = graphics;
            Content = content;
        }

        public static void AddGameState(int stateIndex, GameState state)
        {
            _states.Add(stateIndex, state);
        }

        public static void LoadAssets(string[] textureNames, string[] fontNames, string[] soundEffectNames, string[] songNames)
        {
            foreach (string name in textureNames)
            {
                LoadedTextures.Add(name, Content.Load<Texture2D>(name));
            }
            foreach (string name in fontNames)
            {
                LoadedFonts.Add(name, Content.Load<SpriteFont>(name));
            }
            foreach (string name in soundEffectNames)
            {
                LoadedSoundEffects.Add(name, Content.Load<SoundEffect>(name));
            }
            foreach (string name in songNames)
            {
                LoadedSongs.Add(name, Content.Load<Song>(name));
            }
        }

        public static void RemoveGameState(int stateIndex)
        {
            GameState state = _states[stateIndex];
            _states.Remove(stateIndex);
        }

        public static void Update(GameTime gameTime) {
            foreach (GameState state in _states.Values)
            {
                state.Update(gameTime);
            }
            foreach (GameState state in _states.Values)
            {
                state.PostUpdate(gameTime);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (GameState state in _states.Values) {
                state.Draw(gameTime, spriteBatch);
            }
        }

        public static Texture2D CreateTexture(int width, int height)
        {
            return new Texture2D(_graphics.GraphicsDevice, width, height);
        }
    }
}
