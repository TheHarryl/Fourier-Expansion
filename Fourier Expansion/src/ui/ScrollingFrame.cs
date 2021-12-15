using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class ScrollingFrame : Frame
    {
        #region Fields

        protected Vector2 _scrollSize;
        protected float _scrollBarSize;
        protected Color _scrollBarColor;
        protected EasingStyle _scrollAnimation;
        protected bool _scrollBarAlwaysVisible;

        #endregion

        #region Methods

        public ScrollingFrame(List<UIObject> children, Vector2 position, Vector2 frameSize, Vector2 scrollSize, Color color, float scrollBarSize, Color scrollBarColor, bool scrollBarAlwaysVisible = true,  EasingStyle scrollAnimation = EasingStyle.Quad, int cornerRadius = 0) : base(children, position, frameSize, color, cornerRadius)
        {
            _scrollSize = scrollSize;
            _scrollBarSize = scrollBarSize;
            _scrollBarColor = scrollBarColor;
            _scrollBarAlwaysVisible = scrollBarAlwaysVisible;
            _scrollAnimation = scrollAnimation;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            base.Update(gameTime, offset);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            
        }

        #endregion
    }
}