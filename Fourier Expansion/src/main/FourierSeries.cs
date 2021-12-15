using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class FourierSeries
    {
        #region Fields

        private List<float> _input;
        private float _length;
        private int _n;

        #endregion

        #region Methods

        public FourierSeries(List<float> input, int n)
        {
            _length = 0f;
            for (int i = 0; i < input.Count; i++)
            {
                _length += Math.Abs(input[i]);
            }
            _input = input;
            _n = n;
        }

        private float Integrate(Func<float, float> function)
        {
            float sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += function(i / 1000f) / 1000f;
            }
            return sum;
        }

        public float PolygonAt(float t)
        {
            float distanceAtTime = _length * t;
            float lastDistance = 0f;
            for (int i = 0; i < _input.Count; i++)
            {
                float lastPoint = i == 0 ? _input[_input.Count - 1] : _input[i - 1];
                float difference = Math.Abs(_input[i] - lastPoint);
                if (lastDistance + difference >= distanceAtTime)
                {
                    float lerp = (lastDistance - distanceAtTime) / difference;
                    return lastPoint + _input[i] * lerp;
                }
                lastDistance += difference;
            }
            return lastDistance;
        }

        public Vector2 At(float t)
        {
            Vector2 sum = new Vector2();
            for (int i = 0; i < _n; i++)
            {
                int n = (i % 2 == 0 ? -i / 2 : (i + 1) / 2);
                sum.X += Integrate(t => PolygonAt(t) * (float)Math.Cos(-2 * Math.PI * n * t));
                sum.Y += Integrate(t => PolygonAt(t) * (float)Math.Sin(-2 * Math.PI * n * t));
            }
            return sum;
        }

        #endregion
    }
}