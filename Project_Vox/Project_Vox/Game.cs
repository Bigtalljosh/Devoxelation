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

    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Allows the use of multiple screens and to use the global input management
        ScreenManager screenManager;
        InputManager inputManager;

        //Globals
        public static int _Index;
        public static int _index { get { return _Index; } set { _Index = value; } }
        public static String _Path;
        public static String _path { get { return _Path; } set { _Path = value; } }
        public static KeybindingsConfig KeyBindings;
        public static KeybindingsConfig keyBindings { get { return KeyBindings; } set { KeyBindings = value; } }

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            // Initialises the Resolution class, allowing the game to auto-scale all assets based on the resolution.
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;

            // Sets up the path to the content folder based on the .exe local location
            _index = Assembly.GetExecutingAssembly().Location.LastIndexOf("\\");
            _path = Assembly.GetExecutingAssembly().Location.Substring(0, _index);

            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // Sets the application settings based on the values of the XML file.
            // Some of the values have to be converted to a different type as when they are read
            // they are all read in as Strings. Of course this then doesn't match the intended type.
            Window.Title = appConfigXML.SelectSingleNode("//ScreenTitle").InnerText;
            int selectedResolutionWidth = Convert.ToInt16(appConfigXML.SelectSingleNode("//ScreenWidth").InnerText);
            int selectedResolutionHeight = Convert.ToInt16(appConfigXML.SelectSingleNode("//ScreenHeight").InnerText);
            bool selectedFullScreen = Convert.ToBoolean(appConfigXML.SelectSingleNode("//FullScreen").InnerText);
            //bool selectedFullScreen = false;

            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(1280, 720); // This is the default resolution.. do not change this or you'll break everything!
            Resolution.SetResolution(selectedResolutionWidth, selectedResolutionHeight, selectedFullScreen);

            screenManager = new ScreenManager(this);
            inputManager = new InputManager();
            Components.Add(screenManager);
        }

        protected override void Initialize()
        {
 //           screenManager.AddScreen(new EngineSplash());

            screenManager.AddScreen(new StartMenu());
            //screenManager.AddScreen(new Version5());
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
        }
    }
}
