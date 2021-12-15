using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Fourier_Expansion
{
    public class Particle : GameObject
    {
        protected Sprite _sprite;
        protected Color _startingColor;
        protected Color _endingColor;
        protected double _duration;
        protected double _timeLeft;

        public Particle(Sprite sprite, Color startingColor, Color endingColor, double duration, Vector2 position = new Vector2()) : base(position)
        {
            _sprite = sprite;
            _startingColor = startingColor;
            _endingColor = endingColor;
            _duration = duration;
            _timeLeft = duration;
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            _timeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeLeft <= 0)
                pendingDeletion = true;
            _sprite.Update(gameTime, _position + offset);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            _sprite.Draw(spriteBatch, Color.Lerp(_startingColor, _endingColor, (float)(1.0 - _timeLeft / _duration)), _position + offset);
        }
    }
}