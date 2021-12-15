using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Fourier_Expansion
{
    public enum EasingStyle
    {
        Linear = 0,
        Sine = 1,
        Back = 2,
        Quad = 3,
        Quart = 4,
        Quint = 5,
        Bounce = 6,
        Elastic = 7,
        Exponential = 8,
        Circular = 9,
        Cubic = 10
    }

    public enum EasingDirection
    {
        In = 0,
        Out = 1,
        InOut = 2,
        OutIn = 3
    }

    public class Tween
    {
        #region Fields

        public Vector2 Start;
        public Vector2 End;
        private EasingDirection _easingDirection;
        private EasingStyle _easingStyle;
        private float _duration;
        private double _startTimestamp;

        #endregion

        #region Methods

        public Tween(Vector2 start, Vector2 end, double timestampInSeconds, float duration, EasingDirection easingDirection, EasingStyle easingStyle)
        {
            Start = start;
            End = end;
            _easingDirection = easingDirection;
            _easingStyle = easingStyle;
            _duration = duration;
            _startTimestamp = timestampInSeconds;
        }

        public Vector2 Now(GameTime gameTime)
        {
            if (!Active(gameTime))
                return End;
            else if (gameTime.TotalGameTime.TotalSeconds < _startTimestamp)
                return Start;

            // https://www.desmos.com/calculator/m8myals511

            float x = (float)(gameTime.TotalGameTime.TotalSeconds - _startTimestamp) / _duration;
            if (_easingStyle == EasingStyle.Sine)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Sin(Math.PI * x / 2 - Math.PI / 2) + 1;
                } else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Sin(Math.PI * x / 2);
                } else if (_easingDirection == EasingDirection.InOut)
                {
                    x = (float)Math.Sin(Math.PI * x - Math.PI / 2) / 2f + 0.5f;
                } else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5f ? (float)Math.Sin(Math.PI * x) / 2f : (float)Math.Sin(Math.PI * x - Math.PI) / 2f + 1f;
                }
            } else if (_easingStyle == EasingStyle.Back)
            {
                float s = 1.70158f;
                float s1 = 2.5949095f;
                if (_easingDirection == EasingDirection.In)
                {
                    x = x * x * (x * (s + 1f) - s);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Pow(x - 1f, 2.0) * ((x - 1f) * (s + 1f) + s) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 2f * x * x * (2 * x * (s1 + 1f) - s1) : 0.5f * (float)Math.Pow(2f * x - 2, 2.0) * ((2f * x - 2f) * (s1 + 1f) + s1) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = 0.5f * (float)Math.Pow(2f * x - 1f, 2.0) * ((2f * x - 1f) * (s1 + 1f) + s1 * (x <= 0.5f ? 1f : -1f)) + 0.5f;
                }
            } else if (_easingStyle == EasingStyle.Quad)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Pow(x, 2.0);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = -(float)Math.Pow(x - 1f, 2.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 2f * (float)Math.Pow(x, 2.0) : -2 * (float)Math.Pow(x - 1f, 2.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5f ? -2f * (float)Math.Pow(x - 0.5f, 2.0) + 0.5f : 2f * (float)Math.Pow(x - 0.5f, 2.0) + 0.5f;
                }
            } else if (_easingStyle == EasingStyle.Quart)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Pow(x, 4.0);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = -(float)Math.Pow(x - 1f, 4.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 8f * (float)Math.Pow(x, 4.0) : -8f * (float)Math.Pow(x - 1f, 4.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5f ? -8f * (float)Math.Pow(x - 0.5f, 4.0) + 0.5f : 8f * (float)Math.Pow(x - 0.5f, 4.0) + 0.5f;
                }
            } else if (_easingStyle == EasingStyle.Quint)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Pow(x, 5.0);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Pow(x - 1f, 5.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 16f * (float)Math.Pow(x, 5.0) : 16f * (float)Math.Pow(x - 1, 5.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = 16f * (float)Math.Pow(x - 0.5f, 5.0) + 0.5f;
                }
            } else if (_easingStyle == EasingStyle.Bounce)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    if (x <= 0.25 / 2.75)
                        x = -7.5625f * (float)Math.Pow(1.0 - x - 2.625 / 2.75, 2.0) + 0.015625f;
                    else if (x <= 0.75 / 2.75)
                        x = -7.5625f * (float)Math.Pow(1.0 - x - 2.25 / 2.75, 2.0) + 0.0625f;
                    else if (x <= 1.75 / 2.75)
                        x = -7.5625f * (float)Math.Pow(1.0 - x - 1.5 / 2.75, 2.0) + 0.25f;
                    else
                        x = 1f - 7.5625f * (float)Math.Pow(1.0 - x, 2.0);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    if (x <= 1.0 / 2.75)
                        x = 7.5625f * x * x;
                    else if (x <= 2.0 / 2.75)
                        x = 7.5625f * (float)Math.Pow(x - 1.5 / 2.75, 2.0) + 0.75f;
                    else if (x <= 2.5 / 2.75)
                        x = 7.5625f * (float)Math.Pow(x - 2.25 / 2.75, 2.0) + 0.9375f;
                    else
                        x = 7.5625f * (float)Math.Pow(x - 2.625 / 2.75, 2.0) + 0.984375f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    //x = 
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    //x = 
                }
            } else if (_easingStyle == EasingStyle.Elastic)
            {
                float p = 0.3f;
                float p1 = 0.45f;
                if (_easingDirection == EasingDirection.In)
                {
                    x = -(float)Math.Pow(2.0, 10.0 * (x - 1.0)) * (float)Math.Sin(2.0 * Math.PI * (x - 1.0 - p / 4.0) / p);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Pow(2.0, -10.0 * x) * (float)Math.Sin(2.0 * Math.PI * (x - p / 4.0) / p) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? -0.5f * (float)Math.Pow(2.0, 20.0 * x - 10.0) * (float)Math.Sin(2.0 * Math.PI * (2.0 * x - 1.1125) / p1) : 0.5f * (float)Math.Pow(2.0, -20.0 * x + 10.0) * (float)Math.Sin(2.0 * Math.PI * (2.0 * x - 1.1125) / p1) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5f ? 0.5f * (float)Math.Pow(2.0, -20.0 * x) * (float)Math.Sin(2.0 * Math.PI * (2.0 * x - p1 / 4.0) / p1) + 0.5f : -0.5f * (float)Math.Pow(2.0, 10.0 * (2.0 * x - 2.0)) * (float)Math.Sin(2.0 * Math.PI * (2.0 * x - 2.0 - p1 / 4.0) / p1) + 0.5f;
                }
            } else if (_easingStyle == EasingStyle.Exponential)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Pow(2.0, 10.0 * x - 10.0) - 0.001f;
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = -1.001f * (float)Math.Pow(2.0, -10.0 * x) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 0.5f * (float)Math.Pow(2.0, 20.0 * x - 10.0) - 0.0005f : 0.50025f * -(float)Math.Pow(2.0, -20.0 * x + 10.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5f ? 0.5005f * -(float)Math.Pow(2.0, -20.0 * x) + 0.5005f : 0.5f * (float)Math.Pow(2.0, 10.0 * (2.0 * x - 2.0)) + 0.4995f;
                }
            } else if (_easingStyle == EasingStyle.Circular)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = -(float)Math.Pow(1.0 - Math.Pow(x, 2.0), 0.5) + 1f;
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Pow(-Math.Pow(x - 1.0, 2.0) + 1.0, 0.5);
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5 ? -(float)Math.Pow(-Math.Pow(x, 2.0) + 0.25, 0.5) + 0.5f : (float)Math.Pow(-Math.Pow(x - 1.0, 2.0) + 0.25, 0.5) + 0.5f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = x <= 0.5 ? (float)Math.Pow(-Math.Pow(x - 0.5, 2.0) + 0.25, 0.5) : -(float)Math.Pow(-Math.Pow(x - 0.5, 2.0) + 0.25, 0.5) + 1f;
                }
            } else if (_easingStyle == EasingStyle.Cubic)
            {
                if (_easingDirection == EasingDirection.In)
                {
                    x = (float)Math.Pow(x, 3.0);
                }
                else if (_easingDirection == EasingDirection.Out)
                {
                    x = (float)Math.Pow(x - 1f, 3.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.InOut)
                {
                    x = x <= 0.5f ? 4f * (float)Math.Pow(x, 3.0) : 4f * (float)Math.Pow(x - 1f, 3.0) + 1f;
                }
                else if (_easingDirection == EasingDirection.OutIn)
                {
                    x = 4f * (float)Math.Pow(x - 0.5f, 3.0) + 0.5f;
                }
            }
            return Vector2.Lerp(Start, End, x);
        }

        public bool Active(GameTime gameTime)
        {
            return gameTime.TotalGameTime.TotalSeconds - _startTimestamp < _duration;
        }

        #endregion
    }
}