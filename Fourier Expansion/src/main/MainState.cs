using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fourier_Expansion
{
    public class MainState : GameState
    {
        private List<UIObject> _ui;
        private int _n = 1;
        private bool _pressing = false;
        private bool _nChange = false;
        private List<Vector2> _curve = new List<Vector2>() {
            /*new Vector2(2.5f * 20, -2.5f * 20),
            new Vector2(5f * 20, -5f * 20),
            new Vector2(5f * 20, -10f * 20),
            new Vector2(0f * 20, -15f * 20),
            new Vector2(-5f * 20, -10f * 20),
            new Vector2(-5f * 20, -5f * 20),
            new Vector2(0f * 20, 0f * 20),
            new Vector2(0f * 20, 5f * 20),
            new Vector2(0f * 20, 0f * 20),
            new Vector2(-2.5f * 20, -2.5f * 20),
            new Vector2(-5f * 20, 0f * 20),
            new Vector2(-5f * 20, 5f * 20),
            new Vector2(0f * 20, 10f * 20),
            new Vector2(5f * 20, 5f * 20),
            new Vector2(5f * 20, 0f * 20),
            new Vector2(0f * 20, -5f * 20),
            new Vector2(0f * 20, -10f * 20),
            new Vector2(0f * 20, -5f * 20)*/
            new Vector2(-200f, -200f),
            new Vector2(-200f, 200f),
            new Vector2(200f, 200f),
            new Vector2(200f, -200f)
            /*new Vector2(1.01f * 50f, 6.97f * 50f),
            new Vector2(-2.62f * 50f, 4.74f * 50f),
            new Vector2(-2.5f * 50f, 2.9f * 50f),
            new Vector2(-0.95f * 50f, 1.87f * 50f),
            new Vector2(3.03f * 50f, 3.46f * 50f),
            new Vector2(2.73f * 50f, 4.51f * 50f),
            new Vector2(-1.09f * 50f, 3.44f * 50f),
            new Vector2(-1.44f * 50f, 4.02f * 50f),
            new Vector2(2.51f * 50f, 6.37f * 50f),
            new Vector2(2.45f * 50f, 8.21f * 50f),
            new Vector2(-0.67f * 50f, 8.87f * 50f),
            new Vector2(-3.3f * 50f, 7.86f * 50f),
            new Vector2(-2.86f * 50f, 6.49f * 50f),
            new Vector2(-0.27f * 50f, 7.38f * 50f)*/
        };

        public MainState() : base()
        {
            _textureNames = new string[] {
                "rectangle"
            };
            _fontNames = new string[] {
                "Arial"
            };
            _soundEffectNames = new string[] {

            };
            _songNames = new string[] {
                
            };

            InternalManager.LoadAssets(_textureNames, _fontNames, _soundEffectNames, _songNames);

            _ui = new List<UIObject>()
            {
                new Fourier(_curve, _n, new Vector2(400, 240)),
            };
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.OemMinus))
            {
                if (!_pressing)
                {
                    _n -= 1;
                    _nChange = true;
                }
                _pressing = true;
            } else if (state.IsKeyDown(Keys.OemPlus))
            {
                if (!_pressing)
                {
                    _n += 1;
                    _nChange = true;
                }
                _pressing = true;
            } else
            {
                _pressing = false;
            }

            if (_nChange)
            {
                System.Diagnostics.Debug.WriteLine(_n);
                _ui = new List<UIObject>()
                {
                    new Fourier(_curve, _n, new Vector2(400, 240)),
                };
                _nChange = false;
            }

            for (int i = 0; i < _ui.Count; i++)
            {
                _ui[i].Update(gameTime);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _ui.Count; i++)
            {
                _ui[i].Draw(spriteBatch, new Vector2(0f, 0f));
            }
        }
    }
}