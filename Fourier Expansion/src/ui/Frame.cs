using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class Frame : UIObject
    {
        #region Fields

        protected Texture2D _texture;
        protected Color[] _data;

        private Color _color;
        private int _cornerRadius;

        private List<UIObject> _children;
        public List<UIObject> Children
        {
            get => _children;
            set
            {
                _children = value;
                for (int i = 0; i < _children.Count; i++)
                {
                    _children[i].Parent = this;
                }
            }
        }

        public Vector2 Size {
            get => _size;
            set
            {
                _size = value;
                GenerateTexture();
            }
        }

        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = value;
                if (_cornerRadius > Size.X / 2)
                    _cornerRadius = (int)(Size.X / 2);
                if (_cornerRadius > Size.Y / 2)
                    _cornerRadius = (int)(Size.Y / 2);
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

        public Frame(List<UIObject> children, Vector2 position, Vector2 size, Color color, int cornerRadius = 0) : base(position)
        {
            Children = children;
            _color = color;
            Size = size;
            CornerRadius = cornerRadius;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            base.Update(gameTime, offset);
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(gameTime, offset + Position);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_texture, _position + offset, _color);
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(spriteBatch, offset + Position);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 topLeftPadding, Vector2 bottomRightPadding, Vector2 offset = new Vector2())
        {
            spriteBatch.Draw(_texture, _position + offset, new Rectangle((int)topLeftPadding.X, (int)topLeftPadding.Y, (int)(Size - bottomRightPadding).X, (int)(Size - bottomRightPadding).Y), _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(spriteBatch, offset + Position);
            }
        }

        protected void GenerateTexture()
        {
            _texture = InternalManager.CreateTexture((int)Size.X, (int)Size.Y);
            _data = new Color[(int)Size.X * (int)Size.Y];
            if (CornerRadius == 0)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    for (int x = 0; x < Size.X; x++)
                    {
                        _data[x + y * (int)Size.X] = Color.White;
                    }
                }
            }
            else
            {
                for (int y = 0; y < CornerRadius; y++)
                {
                    for (int x = 0; x < CornerRadius; x++)
                    {
                        double distanceFromCorner = Math.Pow(Math.Pow(CornerRadius - y, 2) + Math.Pow(CornerRadius - x, 2), 0.5) - CornerRadius;
                        if (Math.Abs(distanceFromCorner) <= 1)
                        {
                            _data[x + y * (int)Size.X] = Color.White * (1f - (float)distanceFromCorner);
                            _data[((int)Size.X - x) + y * (int)Size.X - 1] = Color.White * (1f - (float)distanceFromCorner);
                            _data[x + ((int)Size.Y - y - 1) * (int)Size.X] = Color.White * (1f - (float)distanceFromCorner);
                            _data[((int)Size.X - x) + ((int)Size.Y - y - 1) * (int)Size.X - 1] = Color.White * (1f - (float)distanceFromCorner);
                        }
                        else if (distanceFromCorner < 0)
                        {
                            _data[x + y * (int)Size.X] = Color.White;
                            _data[((int)Size.X - x) + y * (int)Size.X - 1] = Color.White;
                            _data[x + ((int)Size.Y - y - 1) * (int)Size.X] = Color.White;
                            _data[((int)Size.X - x) + ((int)Size.Y - y - 1) * (int)Size.X - 1] = Color.White;
                        }
                    }
                }
                if (CornerRadius * 2 < Size.X)
                {
                    for (int y = 0; y < CornerRadius; y++)
                    {
                        for (int x = CornerRadius; x < Size.X - CornerRadius; x++)
                        {
                            _data[x + y * (int)Size.X] = Color.White;
                            _data[x + (y + (int)Size.Y - CornerRadius) * (int)Size.X] = Color.White;
                        }
                    }
                }
                if (CornerRadius * 2 < Size.Y)
                {
                    for (int y = CornerRadius; y < Size.Y - CornerRadius; y++)
                    {
                        for (int x = 0; x < Size.X; x++)
                        {
                            _data[x + y * (int)Size.X] = Color.White;
                        }
                    }
                }
            }
            _texture.SetData(_data);
        }

        #endregion
    }
}