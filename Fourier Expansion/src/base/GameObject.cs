using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fourier_Expansion
{
    public abstract class GameObject
    {
        #region Fields

        protected Vector2 _position;

        public bool pendingDeletion = false;

        #endregion

        #region Methods
        
        public GameObject(Vector2 position = new Vector2())
        {
            _position = position;
        }

        public abstract void Update(GameTime gameTime, Vector2 offset = new Vector2());

        public abstract void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2());

        public virtual Vector2 GetPosition()
        {
            return _position;
        }

        #endregion
    }
}