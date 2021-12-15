using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fourier_Expansion
{
    public class Sprite : GameObject
    {
        #region Fields

        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;
        protected bool _reversedX;
        protected bool _reversedY;

        #endregion

        #region Methods

        public Sprite(string textureName, Rectangle spriteRectangle, Vector2 position = new Vector2()) : base(position)
        {
            _texture = InternalManager.LoadedTextures[textureName];
            _sourceRectangle = spriteRectangle;
        }

        public Sprite(Texture2D texture, Rectangle spriteRectangle, Vector2 position = new Vector2()) : base(position)
        {
            _texture = texture;
            _sourceRectangle = spriteRectangle;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_texture, _position + offset, _sourceRectangle, Color.White, 0f, new Vector2(), 1f, (_reversedX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (_reversedY ? SpriteEffects.FlipVertically : SpriteEffects.None), 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Color color, Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_texture, _position + offset, _sourceRectangle, color, 0f, new Vector2(), 1f, (_reversedX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (_reversedY ? SpriteEffects.FlipVertically : SpriteEffects.None), 0f);
        }

        public Sprite Clone()
        {
            return new Sprite(_texture, _sourceRectangle, _position);
        }

        public void ReverseX(bool toggle)
        {
            _reversedX = toggle;
        }

        public void ReverseY(bool toggle)
        {
            _reversedY = toggle;
        }

        #endregion
    }
}