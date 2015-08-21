/* 
 * Created by Josh Dadak (d005578a) http://www.devoxelation.com
 * As part of Final Year Project at Staffordshire University
 * "Performance of Destructible Game Environments with Voxel Engines"
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Reflection;

using SharedContent;

namespace Devoxelation
{
    public class InputManager
    {
        static private MouseState CurrentMouseState, LastMouseState;
        static private KeyboardState CurrentKeyState, LastKeyState;
        static private GamePadState CurrentPadState, LastPadState;
        static private Vector2 deltaMouse, lastPos, curPos;
        static private Vector2 leftStickPos, rightStickPos;

        public void ProcessKeybindings()
        {
            GameServiceContainer services = new GameServiceContainer();
            ContentManager Content = new ContentManager(services);
            Content.RootDirectory = "Content";

            Game.KeyBindings = Content.Load<KeybindingsConfig>("KeybindingsSettings");
        }

        // Menu Options
        public bool MenuSelect { get { return IsNewKeyPress(Game.KeyBindings.KBMenuSelect) || IsNewButtonPress(Game.KeyBindings.GPMenuSelect); } }
        public bool MenuCancel { get { return IsNewKeyPress(Game.KeyBindings.KBMenuCancel) || IsNewButtonPress(Game.KeyBindings.GPMenuCancel); } }
        public bool MoveMenuUp { get { return IsNewKeyPress(Game.KeyBindings.KBMenuUp); } }
        public bool MoveMenuDown { get { return IsNewKeyPress(Game.KeyBindings.KBMenuDown); } }

        // Player Options 
        public bool PlayerMoveUp { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerMoveUp); } }
        public bool PlayerMoveDown { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerMoveDown); } }
        public bool PlayerMoveLeft { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerMoveLeft); } }
        public bool PlayerMoveRight { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerMoveRight); } }
        public bool PlayerAction01 { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerAction01) || IsNewButtonPress(Game.KeyBindings.GPPlayerAction01); } }
        public bool PlayerAction02 { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerAction02) || IsNewButtonPress(Game.KeyBindings.GPPlayerAction02); } }
        public bool PlayerAction03 { get { return IsNewKeyPress(Game.KeyBindings.KBPlayerAction03) || IsNewButtonPress(Game.KeyBindings.GPPlayerAction03); } }

        // Interaction Demo Options
        public bool EnableEditUI { get { return IsNewKeyPress(Game.KeyBindings.KBEnableEditUI); } }
        public bool DisableEditUI { get { return IsNewKeyPress(Game.KeyBindings.KBDisableEditUI); } }

        // Different button states
        private bool IsNewKeyPress(Keys key) { return CurrentKeyState.IsKeyDown(key) && LastKeyState.IsKeyUp(key); }
        private bool IsHeldKey(Keys key) { return CurrentKeyState.IsKeyDown(key); }
        private bool IsNewButtonPress(Buttons button) { return CurrentPadState.IsButtonDown(button) && LastPadState.IsButtonUp(button); }
        private bool IsHeldButton(Buttons button) { return CurrentPadState.IsButtonDown(button); }

        public void Update(GameTime gameTime)
        {
            LastKeyState = CurrentKeyState;
            LastMouseState = CurrentMouseState;
            LastPadState = CurrentPadState;
            CurrentKeyState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            CurrentPadState = GamePad.GetState(PlayerIndex.One);

            leftStickPos.X = CurrentPadState.ThumbSticks.Left.X;
            leftStickPos.Y = CurrentPadState.ThumbSticks.Left.Y;
            rightStickPos.X = CurrentPadState.ThumbSticks.Right.X;
            rightStickPos.Y = CurrentPadState.ThumbSticks.Right.Y;

            lastPos = curPos;
            curPos = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
            deltaMouse = curPos - lastPos;
            ProcessKeybindings();
        }
    }
}
