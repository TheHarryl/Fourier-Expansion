using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Fourier_Expansion
{
    public class Fourier : Frame
    {
        #region Fields

        // (x px, y px)
        private List<Vector2> _points;
        private float _length;

        private int _n;
        private List<Vector2> _vectors;
        private List<Vector2> _vectorPoints;

        private List<Vector2> _polygonPoints;
        private List<Vector2> _fourierPoints;
        private Vector2 _currentPoint;

        #endregion

        #region Methods

        public Fourier(List<Vector2> points, int num, Vector2 position) : base(new List<UIObject>(), position, new Vector2(1f, 1f), Color.White)
        {
            _points = points;
            _n = num;

            for (int i = 0; i < _points.Count; i++)
            {
                _length += Vector2.Distance(i == 0 ? _points[_points.Count - 1] : _points[i - 1], _points[i]);
            }

            _vectorPoints = new List<Vector2>();
            _currentPoint = new Vector2();
            _vectors = new List<Vector2>();
            for (int i = 0; i < _n; i++)
            {
                int n = (i % 2 == 0 ? i / 2 : -(i + 1) / 2);
                Vector2 vector = new Vector2(
                    Integrate(t => 
                        GetPolygonXAtTime(t) * (float)Math.Cos(-2 * Math.PI * n * t)
                    ) + Integrate(t =>
                        GetPolygonYAtTime(t) * (float)Math.Sin(-2 * Math.PI * n * t)
                    ),
                    Integrate(t =>
                        GetPolygonXAtTime(t) * (float)Math.Sin(-2 * Math.PI * n * t)
                    ) - Integrate(t =>
                        GetPolygonYAtTime(t) * (float)Math.Cos(-2 * Math.PI * n * t)
                    )
                );
                _vectors.Add(vector);
            }
            System.Diagnostics.Debug.WriteLine(_vectors.Count);
            Graph();
        }

        private Vector2 GetPolygonAtTime(float t)
        {
            float distanceAtTime = _length * t;
            float lastDistance = 0f;
            for (int i = 0; i < _points.Count; i++)
            {
                Vector2 lastPoint = i == 0 ? _points[_points.Count - 1] : _points[i - 1];
                float difference = Vector2.Distance(lastPoint, _points[i]);
                if (lastDistance + difference >= distanceAtTime)
                {
                    float lerp = (distanceAtTime - lastDistance) / difference;
                    return Vector2.Lerp(lastPoint, _points[i], lerp);
                }
                lastDistance += difference;
            }
            return new Vector2();
        }

        private float GetPolygonXAtTime(float t)
        {
            return GetPolygonAtTime(t).X;
        }

        private float GetPolygonYAtTime(float t)
        {
            return GetPolygonAtTime(t).Y;
        }

        private Vector2 GetFourierAtTime(float t)
        {
            Vector2 currentPosition = new Vector2();
            for (int i = 0; i < _vectors.Count; i++)
            {
                Vector2 vector = _vectors[i];
                double offset;
                if (vector.X > 0f)
                    offset = Math.Atan(vector.Y / vector.X);
                else if (vector.X == 0f)
                    offset = Math.PI / 2 * Math.Sign(vector.Y);
                else
                    offset = Math.PI + Math.Atan(vector.Y / vector.X);
                int n = (i % 2 == 0 ? i / 2 : -(i + 1) / 2);
                currentPosition.X += vector.Length() * (float)Math.Cos(2 * Math.PI * n * t - offset);
                currentPosition.Y += vector.Length() * (float)Math.Sin(2 * Math.PI * n * t - offset);
            }
            return currentPosition;
        }

        private List<Vector2> GetFourierPointsAtTime(float t)
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2());
            for (int i = 0; i < _vectors.Count; i++)
            {
                Vector2 vector = _vectors[i];
                double offset;
                if (vector.X > 0f)
                    offset = Math.Atan(vector.Y / vector.X);
                else if (vector.X == 0f)
                    offset = Math.PI / 2 * Math.Sign(vector.Y);
                else
                    offset = Math.PI + Math.Atan(vector.Y / vector.X);
                Vector2 newVector = points[points.Count - 1];
                int n = (i % 2 == 0 ? i / 2 : -(i + 1) / 2);
                newVector.X += vector.Length() * (float)Math.Cos(2 * Math.PI * n * t - offset);
                newVector.Y += vector.Length() * (float)Math.Sin(2 * Math.PI * n * t - offset);
                points.Add(newVector);
            }
            return points;
        }

        private float Integrate(Func<float, float> function)
        {
            float sum = 0;
            for (int i = 0; i < 10000; i++)
            {
                sum += function(i / 10000f) / 10000f;
            }
            return sum;
        }

        private void Graph()
        {
            /*_graph = InternalManager.CreateTexture((int)Size.X, (int)Size.Y);
            _data = new Color[(int)Size.X * (int)Size.Y];
            _graph.SetData(_data);*/
            _fourierPoints = new List<Vector2>();
            _polygonPoints = new List<Vector2>();
            for (int t = 0; t < _length; t++)
            {
                _fourierPoints.Add(GetFourierAtTime(t / (_length)));
                _polygonPoints.Add(GetPolygonAtTime(t / (_length)));
            }
            float sum = 0f;
            for (int i = 0; i < _fourierPoints.Count; i++)
            {
                sum += Vector2.Distance(_fourierPoints[i], _polygonPoints[_polygonPoints.Count - 1 - i]) / _fourierPoints.Count;
            }
            System.Diagnostics.Debug.WriteLine(_length + " " + sum);
        }

        public override void Update(GameTime gameTime, Vector2 offset = new Vector2())
        {
            base.Update(gameTime, offset);

            _currentPoint = GetFourierAtTime((float)gameTime.TotalGameTime.TotalSeconds % 10 / 10f);
            _vectorPoints = GetFourierPointsAtTime((float)gameTime.TotalGameTime.TotalSeconds % 10 / 10f);
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(InternalManager.LoadedTextures["rectangle"], r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            base.Draw(spriteBatch, offset);

            Vector2 position = Position + offset;
            for (int i = 0; i < _fourierPoints.Count; i++)
            {
                DrawLine(spriteBatch, position - _fourierPoints[i], position - _polygonPoints[_polygonPoints.Count - 1 - i], new Color(50, 0, 0), 2);
            }
            for (int i = 1; i < _polygonPoints.Count; i++)
            {
                DrawLine(spriteBatch, position - _polygonPoints[i - 1], position - _polygonPoints[i], Color.Red);
            }
            for (int i = 1; i < _fourierPoints.Count; i++)
            {
                DrawLine(spriteBatch, position - _fourierPoints[i - 1], position - _fourierPoints[i], Color.White, 2);
            }
            //spriteBatch.Draw(InternalManager.LoadedTextures["rectangle"], new Rectangle((int)(Position + offset - _currentPoint).X - 5, (int)(Position + offset - _currentPoint).Y - 5, 11, 11), Color.Red);

            for (int i = 1; i < _vectorPoints.Count; i++)
            {
                DrawLine(spriteBatch, -_vectorPoints[i - 1] + position, -_vectorPoints[i] + position, Color.Red);
            }
        }

        #endregion
    }
}