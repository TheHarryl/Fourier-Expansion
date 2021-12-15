using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fourier_Expansion
{
    public class TestTweenState : GameState
    {
        Frame frame1;
        Frame frame2;
        Frame frame3;
        Frame frame4;
        Frame frame5;
        Frame frame6;
        Frame frame7;
        Frame frame8;
        Frame frame9;
        Frame frame10;
        Frame frame11;
        TextLabel text;
        bool done = false;

        public TestTweenState() : base()
        {
            _textureNames = new string[] {
                "rectangle",
                "square"
            };
            _fontNames = new string[] {
                "Arial"
            };
            _soundEffectNames = new string[] {

            };
            _songNames = new string[] {
                
            };

            InternalManager.LoadAssets(_textureNames, _fontNames, _soundEffectNames, _songNames);

            frame1 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Linear", Color.Gray, Alignment.Center)
            }, new Vector2(100, 20), new Vector2(200, 30), Color.White, 10);
            frame2 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Sine", Color.Gray, Alignment.Center)
            }, new Vector2(100, 60), new Vector2(200, 30), Color.White, 10);
            frame3 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Back", Color.Gray, Alignment.Center)
            }, new Vector2(100, 100), new Vector2(200, 30), Color.White, 10);
            frame4 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Quad", Color.Gray, Alignment.Center)
            }, new Vector2(100, 140), new Vector2(200, 30), Color.White, 10);
            frame5 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Quart", Color.Gray, Alignment.Center)
            }, new Vector2(100, 180), new Vector2(200, 30), Color.White, 10);
            frame6 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Quint", Color.Gray, Alignment.Center)
            }, new Vector2(100, 220), new Vector2(200, 30), Color.White, 10);
            frame7 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Bounce", Color.Gray, Alignment.Center)
            }, new Vector2(100, 260), new Vector2(200, 30), Color.White, 10);
            frame8 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Elastic", Color.Gray, Alignment.Center)
            }, new Vector2(100, 300), new Vector2(200, 30), Color.White, 10);
            frame9 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Exponential", Color.Gray, Alignment.Center)
            }, new Vector2(100, 340), new Vector2(200, 30), Color.White, 10);
            frame10 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Circular", Color.Gray, Alignment.Center)
            }, new Vector2(100, 380), new Vector2(200, 30), Color.White, 10);
            frame11 = new Frame(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 30), "Cubic", Color.Gray, Alignment.Center)
            }, new Vector2(100, 420), new Vector2(200, 30), Color.White, 10);

            text = new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(300, 0), new Vector2(200, 50), "Tweening: In", Color.White, Alignment.Center);

            frame1.TweenPosition(2.0, new Vector2(500, 20), EasingDirection.In, EasingStyle.Linear, 2);
            frame2.TweenPosition(2.0, new Vector2(500, 60), EasingDirection.In, EasingStyle.Sine, 2);
            frame3.TweenPosition(2.0, new Vector2(500, 100), EasingDirection.In, EasingStyle.Back, 2);
            frame4.TweenPosition(2.0, new Vector2(500, 140), EasingDirection.In, EasingStyle.Quad, 2);
            frame5.TweenPosition(2.0, new Vector2(500, 180), EasingDirection.In, EasingStyle.Quart, 2);
            frame6.TweenPosition(2.0, new Vector2(500, 220), EasingDirection.In, EasingStyle.Quint, 2);
            frame7.TweenPosition(2.0, new Vector2(500, 260), EasingDirection.In, EasingStyle.Bounce, 2);
            frame8.TweenPosition(2.0, new Vector2(500, 300), EasingDirection.In, EasingStyle.Elastic, 2);
            frame9.TweenPosition(2.0, new Vector2(500, 340), EasingDirection.In, EasingStyle.Exponential, 2);
            frame10.TweenPosition(2.0, new Vector2(500, 380), EasingDirection.In, EasingStyle.Circular, 2);
            frame11.TweenPosition(2.0, new Vector2(500, 420), EasingDirection.In, EasingStyle.Cubic, 2);
        }

        public override void Update(GameTime gameTime)
        {
            frame1.Update(gameTime);
            frame2.Update(gameTime);
            frame3.Update(gameTime);
            frame4.Update(gameTime);
            frame5.Update(gameTime);
            frame6.Update(gameTime);
            frame7.Update(gameTime);
            frame8.Update(gameTime);
            frame9.Update(gameTime);
            frame10.Update(gameTime);
            frame11.Update(gameTime);
            text.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= 6.0 && !done)
            {
                done = true;
                text.Text = "Tweening: Out";
                frame1.Position = new Vector2(100, 20);
                frame2.Position = new Vector2(100, 60);
                frame3.Position = new Vector2(100, 100);
                frame4.Position = new Vector2(100, 140);
                frame5.Position = new Vector2(100, 180);
                frame6.Position = new Vector2(100, 220);
                frame7.Position = new Vector2(100, 260);
                frame8.Position = new Vector2(100, 300);
                frame9.Position = new Vector2(100, 340);
                frame10.Position = new Vector2(100, 380);
                frame11.Position = new Vector2(100, 420);
                frame1.TweenPosition(8.0, new Vector2(500, 20), EasingDirection.Out, EasingStyle.Linear, 2);
                frame2.TweenPosition(8.0, new Vector2(500, 60), EasingDirection.Out, EasingStyle.Sine, 2);
                frame3.TweenPosition(8.0, new Vector2(500, 100), EasingDirection.Out, EasingStyle.Back, 2);
                frame4.TweenPosition(8.0, new Vector2(500, 140), EasingDirection.Out, EasingStyle.Quad, 2);
                frame5.TweenPosition(8.0, new Vector2(500, 180), EasingDirection.Out, EasingStyle.Quart, 2);
                frame6.TweenPosition(8.0, new Vector2(500, 220), EasingDirection.Out, EasingStyle.Quint, 2);
                frame7.TweenPosition(8.0, new Vector2(500, 260), EasingDirection.Out, EasingStyle.Bounce, 2);
                frame8.TweenPosition(8.0, new Vector2(500, 300), EasingDirection.Out, EasingStyle.Elastic, 2);
                frame9.TweenPosition(8.0, new Vector2(500, 340), EasingDirection.Out, EasingStyle.Exponential, 2);
                frame10.TweenPosition(8.0, new Vector2(500, 380), EasingDirection.Out, EasingStyle.Circular, 2);
                frame11.TweenPosition(8.0, new Vector2(500, 420), EasingDirection.Out, EasingStyle.Cubic, 2);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            frame1.Draw(spriteBatch);
            frame2.Draw(spriteBatch);
            frame3.Draw(spriteBatch);
            frame4.Draw(spriteBatch);
            frame5.Draw(spriteBatch);
            frame6.Draw(spriteBatch);
            frame7.Draw(spriteBatch);
            frame8.Draw(spriteBatch);
            frame9.Draw(spriteBatch);
            frame10.Draw(spriteBatch);
            frame11.Draw(spriteBatch);
            text.Draw(spriteBatch);
        }
    }
}