using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fourier_Expansion
{
    public class TestState : GameState
    {
        Frame frame;
        Button button1;
        Button button2;
        Button button3;
        Frame nested;
        Image image;
        TextBox bruh;

        public TestState() : base()
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

            frame = new Frame(new List<UIObject>()
            {

            }, new Vector2(50, 50), new Vector2(200, 50), Color.Gray, 10);
            button1 = new Button(new List<UIObject>()
            {

            }, new Vector2(50, 150), new Vector2(200, 50), Color.White, 10);
            button2 = new Button(new List<UIObject>()
            {

            }, new Vector2(50, 250), new Vector2(50, 50), Color.White, 25);
            button3 = new Button(new List<UIObject>()
            {
                new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 50), "test", Color.Gray, Alignment.Center)
            }, new Vector2(50, 350), new Vector2(200, 50), Color.White, 10, 0.2f, 0.4f, true);


            nested = new Frame(new List<UIObject>() {
                new Button(new List<UIObject>()
                {
                    new TextLabel(InternalManager.LoadedFonts["Arial"], new Vector2(0, 0), new Vector2(200, 50), "Nested Button", Color.Gray, Alignment.Center)
                }, new Vector2(50, 50), new Vector2(200, 50), Color.White, 10)
            }, new Vector2(300, 50), new Vector2(300, 150), Color.Gray, 20);

            image = new Image(InternalManager.LoadedTextures["square"], new Vector2(300, 300), 1f, Color.White, 50);

            bruh = new TextBox(InternalManager.LoadedFonts["Arial"], new Vector2(300, 400), new Vector2(200, 50), "", "Click here to type", Color.White, Color.LightGray, Color.Gray * 0.5f, Alignment.Left | Alignment.Top, true);
        }

        public override void Update(GameTime gameTime)
        {
            frame.Update(gameTime);
            button1.Update(gameTime);
            button2.Update(gameTime);
            button3.Update(gameTime);
            nested.Update(gameTime);
            image.Update(gameTime);
            bruh.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(InternalManager.LoadedFonts["Arial"], "hundauphd", new Vector2(), Color.White);
            frame.Draw(spriteBatch);
            button1.Draw(spriteBatch);
            button2.Draw(spriteBatch);
            button3.Draw(spriteBatch);
            nested.Draw(spriteBatch);
            image.Draw(spriteBatch);
            bruh.Draw(spriteBatch);
        }
    }
}