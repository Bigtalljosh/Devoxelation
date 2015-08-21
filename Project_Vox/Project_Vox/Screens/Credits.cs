/* 
 * Created by Josh Dadak (d005578a) http://www.devoxelation.com
 * As part of Final Year Project at Staffordshire University
 * "Performance of Destructible Game Environments with Voxel Engines"
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using SharedContent;

namespace Devoxelation
{
    public class Credits : GameScreen
    {
        #region Fields
        SpriteBatch spriteBatch;

        Texture2D backgroundTexture, picture;
        SpriteFont kootenay10Font;

        #endregion

        public Credits()
        {
            // how many seconds do you want the transition to last for?
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        public override void Initialize()
        {
            // initiate the viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;

            backgroundTexture = Content.Load<Texture2D>("Textures\\enginesplash");
            picture = Content.Load<Texture2D>("Textures\\picture");
            kootenay10Font = Content.Load<SpriteFont>("Fonts\\titlemenufont");
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime, bool covered)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.Escape))
            {
                Remove();
                ScreenManager.AddScreen(new MainMenu());
            }
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Resolution.BeginDraw();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(picture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}