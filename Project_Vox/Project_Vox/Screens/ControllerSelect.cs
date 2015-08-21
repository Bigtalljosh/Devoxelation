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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Reflection;

using SharedContent;

namespace Devoxelation
{
    public class ControllerSelectScreen : GameScreen
    {
        ScreensConfig scrConfig;

        Texture2D backgroundTexture;
        Texture2D buttonTexture;
        KeyboardState oldkeyboardState;
        KeyboardState currentKeyboardState;
        string menuSelection = "";
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        public ControllerSelectScreen()
        {

        }

        public override void Initialize()
        {
            currentKeyboardState = new KeyboardState();
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            scrConfig = Content.Load<ScreensConfig>("ScreensSettings");

            TransitionOnTime = TimeSpan.FromSeconds(scrConfig.ControllerDetect_TranOn);
            TransitionOffTime = TimeSpan.FromSeconds(scrConfig.ControllerDetect_TranOff);

            if (gamePadState.IsConnected)
            {
                backgroundTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_BGImage);
                buttonTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_360Image);
            }
            else 
            {
                backgroundTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_BGImage);
                buttonTexture = Content.Load<Texture2D>(scrConfig.ControllerDetect_PCImage);
            }
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;                  // calls the menuinputsystem.cs
            oldkeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Escape) || input.MenuCancel)
            {
                menuSelection = "Escape";
                Remove();
            }
            if (currentKeyboardState.IsKeyDown(Keys.Enter) || input.MenuSelect)
            {
                menuSelection = "Enter";
                Remove();
            }
            else
            {
                menuSelection = "";
                Remove();
            }
            base.Update(gameTime, covered);
        }

        public override void Remove()
        {
            if (menuSelection == "Escape")
            {
                base.Remove();
                ScreenManager.Game.Exit();
            }
            if (menuSelection == "Enter")
            {
                base.Remove();
                ScreenManager.AddScreen(new MainMenu());
            }
            else
            {
                base.Remove();
                ScreenManager.AddScreen(new ControllerSelectScreen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            Vector2 buttonTextureL = new Vector2(478, 486);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(buttonTexture, buttonTextureL, Color.White);
            spriteBatch.End();
        }
    }
}