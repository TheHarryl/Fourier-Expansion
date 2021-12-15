using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class AnimatedSprite : Sprite
    {
        #region Fields

        protected int _rows;
        protected int _columns;
        protected int _frames;
        protected int _currentFrame;
        protected int _row;
        protected int _column;
        protected int _paddingX;
        protected int _paddingY;

        protected int _framerate;

        protected TimeSpan _lastFrameTime;
        protected bool _paused;

        protected bool _done;
        protected bool _looped;

        #endregion

        #region Methods

        public AnimatedSprite(string textureName, Rectangle initialSpriteRectangle, int rows, int columns, int frames, int framerate, int paddingX = 0, int paddingY = 0, Vector2 position = new Vector2(), bool looped = true) : base(textureName, initialSpriteRectangle, position)
        {
            _rows = rows;
            _columns = columns;
            _frames = frames;
            _framerate = framerate;
            _paddingX = paddingX;
            _paddingY = paddingY;
            _looped = looped;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            if (!_paused && (gameTime.TotalGameTime - _lastFrameTime).TotalSeconds >= (1.0 / _framerate))
            {
                _lastFrameTime = gameTime.TotalGameTime;
                _currentFrame++;
                if (_currentFrame == _frames)
                {
                    if (!_looped)
                        return;
                    _row = 0;
                    _column = 0;
                    _currentFrame = 0;
                    _done = true;
                }
                else
                {
                    _row = _row + 1 >= _rows ? 0 : _row + 1;
                    if (_row == 0)
                    {
                        _column++;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            int width = (_sourceRectangle.Right - _sourceRectangle.Left);
            int height = (_sourceRectangle.Bottom - _sourceRectangle.Top);
            spriteBatch.Draw(
                _texture,
                _position + offset,
                new Rectangle(_sourceRectangle.Left + (width + _paddingX) * _row,
                _sourceRectangle.Top + (height + _paddingY) * _column,
                width, height),
                Color.White,
                0f,
                new Vector2(),
                1f,
                (_reversedX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (_reversedY ? SpriteEffects.FlipVertically : SpriteEffects.None),
                0f
            );
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public void SetFramerate(int framerate)
        {
            _framerate = framerate;
        }
        
        public bool IsDone()
        {
            return _done;
        }

        #endregion
    }
}