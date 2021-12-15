using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class Button : Frame
    {
        #region Fields

        protected float _hoverDarken;
        protected float _clickDarken;
        protected bool _pressed;
        protected bool _overrideCursor;

        public bool Hovering;

        #endregion

        #region Methods

        public Button(List<UIObject> children, Vector2 position, Vector2 size, Color color, int cornerRadius = 0, float hoverDarken = 0.2f, float clickDarken = 0.4f, bool overrideCursor = true) : base(children, position, size, color, cornerRadius)
        {
            _hoverDarken = hoverDarken;
            _clickDarken = clickDarken;
            _overrideCursor = overrideCursor;
        }

        private bool IsHovering(Vector2 offset = new Vector2())
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle hitbox = new Rectangle((int)(Position + offset).X, (int)(Position + offset).Y, (int)Size.X, (int)Size.Y);
            if (!hitbox.Contains(mouseState.X, mouseState.Y))
                return false;
            if (CornerRadius == 0)
                return true;
            return _data[(mouseState.X - (int)(Position + offset).X) + (mouseState.Y - (int)(Position + offset).Y) * (int)Size.X] == Color.White;
        }
        
        protected void OnClickStart(GameTime gameTime, Vector2 offset = new Vector2())
        {
            
        }

        protected void OnClickEnd(GameTime gameTime, Vector2 offset = new Vector2())
        {
            Parent.TweenPosition(gameTime.TotalGameTime.TotalSeconds, new Vector2(300, 300), EasingDirection.Out, EasingStyle.Bounce, 1f);
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            MouseState mouseState = Mouse.GetState();
            if (IsHovering(offset))
            {
                Hovering = true;
                if (_overrideCursor)
                    Mouse.SetCursor(MouseCursor.Hand);
                if (mouseState.LeftButton == ButtonState.Pressed && _pressed == false)
                {
                    _pressed = true;
                    OnClickStart(gameTime, offset);
                }
            }
            else if (Hovering == true)
            {
                Hovering = false;
                if (_overrideCursor)
                    Mouse.SetCursor(MouseCursor.Arrow);
            }
            if (mouseState.LeftButton == ButtonState.Released && _pressed == true)
            {
                _pressed = false;
                if (Hovering)
                    OnClickEnd(gameTime, offset);
            }
            base.Update(gameTime, offset);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            base.Draw(spriteBatch, offset);
            if (_pressed)
            {
                spriteBatch.Draw(_texture, Position + offset, Color.Black * _clickDarken);
            }
            else if (Hovering)
            {
                spriteBatch.Draw(_texture, Position + offset, Color.Black * _hoverDarken);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 topLeftPadding, Vector2 bottomRightPadding, Vector2 offset = new Vector2())
        {
            base.Draw(spriteBatch, offset);
            if (_pressed)
            {
                spriteBatch.Draw(_texture, _position + offset, new Rectangle((int)topLeftPadding.X, (int)topLeftPadding.Y, (int)(Size - bottomRightPadding).X, (int)(Size - bottomRightPadding).Y), Color.Black * _clickDarken);
            }
            else if (Hovering)
            {
                spriteBatch.Draw(_texture, Position + offset, Color.Black * _hoverDarken);
            }
        }

        #endregion
    }
}