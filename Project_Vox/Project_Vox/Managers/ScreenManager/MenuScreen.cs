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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using SharedContent;

namespace Devoxelation
{
    public abstract class MenuScreen : GameScreen
    {
        public static KeybindingsConfig KeyBindings { get; private set; }

        // Sets up a list to store all the strings
        List<String> menuentriesText = new List<String>();
        public List<String> MenuEntriesText { get { return menuentriesText; } }

        SpriteFont menuText;
        public SpriteFont MenuText { get { return menuText; } set { menuText = value; } }

        // Starting position on the screen in X and Y
        Vector2 startPosition;
        public Vector2 StartPosition { get { return startPosition; } set { startPosition = value; } }

        Vector2 currentPosition;
        public Vector2 CurrentPosition { get { return currentPosition; } set { currentPosition = value; } }

        // Colour of the text when the cursor is over it
        public Color selected;
        public Color Selected { get { return selected; } set { selected = value; } }

        // Colour of the text when the cursor isn't on it
        Color unselected;
        public Color UnSelected { get { return unselected; } set { unselected = value; } }

        // Sets up instances to allow sounds to be played on the menu screens
        SoundEffect menuitemsound;
        SoundEffect menuselectsound;
        SoundEffectInstance menuItemSoundInstance;
        SoundEffectInstance menuSelectSoundInstance;

        // Sets up the ability to select a menu item based on it's ID
        int selectedEntry = 0;
        public abstract void MenuSelect(int menuselected);
        public abstract void MenuCancel();

        public MenuScreen()
        {
            // Default time it takes for the screen to fade in and out
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            //menuitemsound = content.Load<SoundEffect>("Audio\\menumove");
            //menuselectsound = content.Load<SoundEffect>("Audio\\menuselect");
            //menuItemSoundInstance = menuitemsound.CreateInstance();
            //menuSelectSoundInstance = menuselectsound.CreateInstance();
        }

        public override void UnloadContent()
        {
            if (menuText != null) menuText = null;
        }

        public override void HandleInput()
        {
            // Loads up the input system that can be used to control the menu
            InputManager input = ScreenManager.InputSystem;
            if (input.MoveMenuUp)
            {
                selectedEntry--;
                //menuItemSoundInstance.Volume = 0.75f;
                //menuItemSoundInstance.Play();
                if (selectedEntry < 0)
                {
                    selectedEntry = menuentriesText.Count - 1;
                }
            }
            if (input.MoveMenuDown)
            {
                selectedEntry++;
                //menuItemSoundInstance.Volume = 0.75f;
                //menuItemSoundInstance.Play();
                if (selectedEntry >= menuentriesText.Count)
                {
                    selectedEntry = 0;
                }
            }
            if (input.MenuSelect)
            {
                //menuSelectSoundInstance.Volume = 0.75f;
                //menuSelectSoundInstance.Play();
                MenuSelect(selectedEntry);
            }
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            base.Update(gameTime, covered);
            currentPosition = new Vector2(startPosition.X, startPosition.Y);
            if (ScreenState == ScreenState.TransitionOn || ScreenState == ScreenState.TransitionOff)
            {
                Vector2 acceleration = new Vector2((float)Math.Pow(TransitionPercent - 1, 2), 0);
                acceleration.X *= TransitionDirection * 150;
                currentPosition += acceleration;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Vector2 menuPosition = new Vector2(currentPosition.X, currentPosition.Y);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            for (int i = 0; i < menuentriesText.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                DrawTextEntry(spriteBatch, gameTime, menuentriesText[i], menuPosition, isSelected);
                menuPosition.Y += menuText.LineSpacing;
            }
            spriteBatch.End();
        }

        private void DrawTextEntry(SpriteBatch spriteBatch, GameTime gameTime, string textEntry, Vector2 position, bool isSelected)
        {
            Vector2 origin = new Vector2(0, menuText.LineSpacing / 2);
            Color color = isSelected ? selected : unselected;
            color = (color * ScreenAlpha);

            float pulse = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3));
            float scale = isSelected ? (1 + pulse * 0.05f) : 0.8f;

            spriteBatch.DrawString(menuText, textEntry, position, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
