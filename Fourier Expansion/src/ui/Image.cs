using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class Image : UIObject
    {
        #region Fields

        protected Texture2D _texture;
        protected Texture2D _roundedTexture;
        protected Color[] _data;

        private Color _color;
        private int _cornerRadius;

        private float _scale;
        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                GenerateTexture();
            }
        }

        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = value;
                if (_cornerRadius > _texture.Width / 2)
                    _cornerRadius = (int)(_texture.Width / 2);
                if (_cornerRadius > _texture.Height / 2)
                    _cornerRadius = (int)(_texture.Height / 2);
                GenerateTexture();
            }
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        #endregion

        #region Methods

        public Image(Texture2D texture, Vector2 position, float scale, Color color, int cornerRadius = 0) : base(position)
        {
            _texture = texture;
            _color = color;
            Scale = scale;
            CornerRadius = cornerRadius;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            if (_positionTween != null)
            {
                _position = _positionTween.Now(gameTime);
            }
            if (_sizeTween != null)
            {
                Scale = _sizeTween.Now(gameTime).X;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_roundedTexture, _position + offset, null, _color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 topLeftPadding = new Vector2(), Vector2 bottomRightPadding = new Vector2(), Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_roundedTexture, _position + offset, new Rectangle((int)topLeftPadding.X, (int)topLeftPadding.Y, (int)(_roundedTexture.Width * Scale - bottomRightPadding.X), (int)(_roundedTexture.Height * Scale - bottomRightPadding.Y)), _color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public void TweenSize(double timestampInSeconds, float endSize, EasingDirection easingDirection = EasingDirection.Out, EasingStyle easingStyle = EasingStyle.Quad, float time = 1f)
        {
            _sizeTween = new Tween(new Vector2(Scale, 0f), new Vector2(endSize, 0f), timestampInSeconds, time, easingDirection, easingStyle);
        }

        protected void GenerateTexture()
        {
            _data = new Color[(int)_texture.Width * (int)_texture.Height];
            _texture.GetData<Color>(_data);
            _roundedTexture = InternalManager.CreateTexture((int)_texture.Width, (int)_texture.Height);
            if (CornerRadius != 0)
            {
                for (int y = 0; y < CornerRadius; y++)
                {
                    for (int x = 0; x < CornerRadius; x++)
                    {
                        double distanceFromCorner = Math.Pow(Math.Pow(CornerRadius - y, 2) + Math.Pow(CornerRadius - x, 2), 0.5) - CornerRadius;
                        if (Math.Abs(distanceFromCorner) <= 1)
                        {
                            _data[x + y * (int)_texture.Width] *= (1f - (float)distanceFromCorner);
                            _data[((int)_texture.Width - x) + y * (int)_texture.Width - 1] *= (1f - (float)distanceFromCorner);
                            _data[x + ((int)_texture.Height - y - 1) * (int)_texture.Width] *= (1f - (float)distanceFromCorner);
                            _data[((int)_texture.Width - x) + ((int)_texture.Height - y - 1) * (int)_texture.Width - 1] *= (1f - (float)distanceFromCorner);
                        } else if (distanceFromCorner > 0)
                        {
                            _data[x + y * (int)_texture.Width] *= 0f;
                            _data[((int)_texture.Width - x) + y * (int)_texture.Width - 1] *= 0f;
                            _data[x + ((int)_texture.Height - y - 1) * (int)_texture.Width] *= 0f;
                            _data[((int)_texture.Width - x) + ((int)_texture.Height - y - 1) * (int)_texture.Width - 1] *= 0f;
                        }
                    }
                }
            }
            _roundedTexture.SetData(_data);
        }

        #endregion
    }
}