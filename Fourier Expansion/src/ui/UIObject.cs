using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class UIObject : GameObject
    {
        #region Fields

        protected Vector2 _size;

        protected Tween _sizeTween;
        protected Tween _positionTween;

        private UIObject _parent;
        public UIObject Parent
        {
            get => _parent != null ? _parent : this;
            set => _parent = value;
        }

        #endregion

        #region Methods

        public UIObject(Vector2 position = new Vector2(), Vector2 size = new Vector2()) : base(position)
        {
            _size = size;
        }

        public override void Update(GameTime gameTime, Vector2 offset = default)
        {
            if (_positionTween != null)
            {
                _position = _positionTween.Now(gameTime);
                if (!_positionTween.Active(gameTime))
                    _positionTween = null;
            }
            if (_sizeTween != null)
            {
                _size = _sizeTween.Now(gameTime);
                if (!_sizeTween.Active(gameTime))
                    _sizeTween = null;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = default)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 topLeftPadding, Vector2 bottomRightPadding, Vector2 offset = new Vector2())
        {
        }

        public virtual void TweenPosition(double timestampInSeconds, Vector2 endPosition, EasingDirection easingDirection = EasingDirection.Out, EasingStyle easingStyle = EasingStyle.Quad, float time = 1f, bool overrides = false)
        {
            if (overrides || _positionTween == null)
                _positionTween = new Tween(_position, endPosition, timestampInSeconds, time, easingDirection, easingStyle);
        }

        public virtual void TweenSize(double timestampInSeconds, Vector2 endSize, EasingDirection easingDirection = EasingDirection.Out, EasingStyle easingStyle = EasingStyle.Quad, float time = 1f, bool overrides = false)
        {
            if (overrides || _sizeTween == null)
                _sizeTween = new Tween(_size, endSize, timestampInSeconds, time, easingDirection, easingStyle);
        }

        public void TweenSizeAndPosition(double timestampInSeconds, Vector2 endSize, Vector2 endPosition, EasingDirection easingDirection = EasingDirection.Out, EasingStyle easingStyle = EasingStyle.Quad, float time = 1f)
        {
            TweenSize(timestampInSeconds, endSize, EasingDirection.Out, EasingStyle.Quad, 1f);
            TweenPosition(timestampInSeconds, endPosition, EasingDirection.Out, EasingStyle.Quad, 1f);
        }

        #endregion
    }
}