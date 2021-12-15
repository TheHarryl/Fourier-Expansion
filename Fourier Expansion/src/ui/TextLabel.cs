using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fourier_Expansion
{
    public enum Alignment {
        Center = 0,
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom = 8
    }

    public class TextLabel : UIObject
    {
        #region Fields

        protected string _text;

        protected SpriteFont _font;
        protected Color _color;
        protected Alignment _align;

        protected Vector2 _pos;
        protected Vector2 _origin;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        public Vector2 Size
        {
            get => _size;
            set => _size = value;
        }
        public virtual string Text
        {
            get => _text;
            set => _text = value;
        }

        #endregion

        #region Methods

        public TextLabel(SpriteFont font, Vector2 position, Vector2 size, string text, Color color, Alignment align, bool wordWrap = true) : base(position, size)
        {
            _font = font;
            _text = text;
            _color = color;
            _align = align;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            base.Update(gameTime, offset);
            Rectangle bounds = new Rectangle((int)(Position + offset).X, (int)(Position + offset).Y, (int)Size.X, (int)Size.Y);
            Vector2 textSize = _font.MeasureString(Text);
            _pos = (Position + offset) + Size / 2;
            _origin = textSize * 0.5f;

            if (_align.HasFlag(Alignment.Left))
                _origin.X += bounds.Width / 2 - textSize.X / 2;

            if (_align.HasFlag(Alignment.Right))
                _origin.X -= bounds.Width / 2 - textSize.X / 2;

            if (_align.HasFlag(Alignment.Top))
                _origin.Y += bounds.Height / 2 - textSize.Y / 2;

            if (_align.HasFlag(Alignment.Bottom))
                _origin.Y -= bounds.Height / 2 - textSize.Y / 2;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            spriteBatch.DrawString(_font, Text, _pos, _color, 0, _origin, 1, SpriteEffects.None, 0);
        }

        #endregion
    }
}