using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public abstract class GameState
    {
        #region Fields

        protected string[] _textureNames;
        protected string[] _fontNames;
        protected string[] _soundEffectNames;
        protected string[] _songNames;

        protected List<GameObject> _objects = new List<GameObject>();

        #endregion

        #region Methods

        public GameState()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject _object in _objects)
                _object.Update(gameTime);
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                if (_objects[i].pendingDeletion)
                    _objects.RemoveAt(i);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (GameObject _object in _objects)
                _object.Draw(spriteBatch);
        }

        public string[] GetTextureNames()
        {
            return _textureNames;
        }

        public string[] GetFontNames()
        {
            return _fontNames;
        }

        public string[] GetSoundEffectNames()
        {
            return _soundEffectNames;
        }

        public string[] GetSongNames()
        {
            return _songNames;
        }

        #endregion
    }
}