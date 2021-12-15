using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Fourier_Expansion
{
    public class InteractableTextLabel : TextLabel
    {
        #region Fields

        private Dictionary<char, SpriteFont.Glyph> _glyphs;
        protected float _fontSize;
        protected bool _pressed;
        private bool _hovering;
        private Color _highlightColor;
        private bool _overrideCursor;
        protected double _lastClick;

        protected int _highlightStartIndex;
        protected int _highlightEndIndex;
        protected bool _selected;

        #endregion

        #region Methods

        public InteractableTextLabel(SpriteFont font, Vector2 position, Vector2 size, string text, Color textColor, Color highlightColor, Alignment align, bool wordWrap = true, bool overrideCursor = false) : base(font, position, size, text, textColor, align, wordWrap)
        {
            _glyphs = font.GetGlyphs();
            _fontSize = font.MeasureString("test").Y;
            _highlightColor = highlightColor;
            _overrideCursor = overrideCursor;
        }

        public int getLetterIndex(Vector2 position, MouseState mouse, bool estimate = false)
        {
            Vector2 lastPosition = position;
            int lastLineBreak = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                char letter = Text[i];
                if (_glyphs.ContainsKey(letter))
                {
                    SpriteFont.Glyph glyph = _glyphs[letter];
                    Rectangle bounds = new Rectangle((int)lastPosition.X, (int)lastPosition.Y, (int)glyph.WidthIncludingBearings, (int)_fontSize);
                    Rectangle boundsLeft = new Rectangle((int)lastPosition.X, (int)lastPosition.Y, (int)Math.Ceiling(glyph.WidthIncludingBearings / 2f), (int)_fontSize);
                    if (bounds.Contains(mouse.X, mouse.Y))
                    {
                        if (boundsLeft.Contains(mouse.X, mouse.Y))
                        {
                            return i;
                        }
                        else
                        {
                            return i + 1;
                        }
                    }
                    lastPosition.X += glyph.WidthIncludingBearings + _font.Spacing;
                }
                else if (letter.Equals('\n'))
                {
                    lastPosition.X = position.X;
                    lastPosition.Y += _font.LineSpacing;
                    if (lastPosition.Y > mouse.Y && estimate)
                        if (i > _highlightStartIndex)
                        {
                            return i;
                        }
                        else
                        {
                            return lastLineBreak;
                        }
                    lastLineBreak = i;
                }
            }
            return -1;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            base.Update(gameTime, offset);

            Vector2 position = _pos - _origin;
            Rectangle textBounds = new Rectangle((int)position.X, (int)position.Y, (int)_font.MeasureString(Text).X, (int)_font.MeasureString(Text).Y);
            MouseState mouse = Mouse.GetState();

            if (textBounds.Contains(mouse.X, mouse.Y))
            {
                _hovering = true;
                int letterIndex = getLetterIndex(position, mouse);
                if (_overrideCursor)
                {
                    if (letterIndex != -1)
                    {
                        Mouse.SetCursor(MouseCursor.IBeam);
                    }
                    else
                    {
                        Mouse.SetCursor(MouseCursor.Arrow);
                    }
                }
                if (_pressed == false && mouse.LeftButton == ButtonState.Pressed)
                {
                    _lastClick = gameTime.TotalGameTime.TotalSeconds;
                    _pressed = true;
                    _highlightStartIndex = letterIndex;
                    _highlightEndIndex = letterIndex;
                    _selected = true;
                }
            }
            else
            {
                if (_hovering == true && _overrideCursor)
                    Mouse.SetCursor(MouseCursor.Arrow);
                _hovering = false;
                if (_pressed == false && mouse.LeftButton == ButtonState.Pressed)
                    _selected = false;
            }
            if (_pressed == true)
            {
                if (mouse.LeftButton == ButtonState.Released)
                    _pressed = false;
                int letterIndex = getLetterIndex(position, mouse, true);
                _highlightEndIndex = letterIndex != -1 ? letterIndex : _text.Length;
            }

            if (_selected)
            {
                KeyboardState state = Keyboard.GetState();
                int min = Math.Min(_highlightStartIndex, _highlightEndIndex);
                int max = Math.Max(_highlightStartIndex, _highlightEndIndex);
                int length = Math.Abs(_highlightStartIndex - _highlightEndIndex);
                if ((state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl)))
                {
                    if (state.IsKeyDown(Keys.A))
                    {
                        _highlightStartIndex = 0;
                        _highlightEndIndex = _text.Length;
                    }
                    if (state.IsKeyDown(Keys.C) && _highlightStartIndex != _highlightEndIndex)
                        Clipboard.SetText(_text.Substring(min, length));
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            Vector2 position = _pos - _origin;

            if (_selected && _highlightStartIndex != _highlightEndIndex)
            {
                Vector2 lastPosition = position;
                for (int i = 0; i < _text.Length; i++)
                {
                    char letter = _text[i];
                    if (_glyphs.ContainsKey(letter))
                    {
                        SpriteFont.Glyph glyph = _glyphs[letter];
                        if (i >= Math.Min(_highlightStartIndex, _highlightEndIndex) && i < Math.Max(_highlightStartIndex, _highlightEndIndex))
                            spriteBatch.Draw(InternalManager.LoadedTextures["rectangle"], new Rectangle((int)lastPosition.X, (int)lastPosition.Y, (int)glyph.WidthIncludingBearings, (int)_fontSize), _highlightColor);
                        lastPosition.X += glyph.WidthIncludingBearings + _font.Spacing;
                    }
                    else if (letter.Equals('\n'))
                    {
                        lastPosition.X = position.X;
                        lastPosition.Y += _font.LineSpacing;
                    }
                }
            }

            base.Draw(spriteBatch, offset);
        }

        #endregion
    }
}