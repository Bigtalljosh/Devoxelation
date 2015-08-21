/* Version 1 is the core version with nothing but basic implementation *
 * 
 */

/* 
 * Created by Josh Dadak (d005578a) http://www.devoxelation.com
 * As part of Final Year Project at Staffordshire University
 * "Performance of Destructible Game Environments with Voxel Engines"
 * 
 */

//The Terrain generation methods DrawCubes() and GenerateMap() were initially taken from the 'CubeCrafter' Project
// URL: http://cubedefense.codeplex.com/SourceControl/changeset/view/16830

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharedContent;

namespace Devoxelation
{
    public class Version1 : GameScreen
    {
        #region Fields
        SpriteBatch spriteBatch;

        #region OnScreen
        //Fonts and Texts
        SpriteFont kootenayFont;
        string fpsText = "fps: ";
        Vector2 fpsTextLocation = new Vector2(10, 10);
        Vector2 fpsLocation = new Vector2(45, 10);
        string drawsText = "Draws: ";
        Vector2 drawsTextLocation = new Vector2(10, 30);
        Vector2 drawsLocation = new Vector2(85, 30);

        //FPS
        int totalFrames = 0;
        float elapsedTime = 0.0f;
        int fps = 0;

        //Draws
        int drawn = 0;
        #endregion 

        #region States
        //Ability to Pause
        enum GameStates
        {
            Normal,
            Paused,
        }
        GameStates GameState;
        #endregion

        #region KeyPressDelay
        //Key Press related
        float KeyPressCheckDelay = 0.2f;
        float TotalElapsedTime = 0;
        #endregion

        private StaticVBRenderer currentVBRenderer;
        private Effect staticVBEffect;

        //Camera Angles / frame rate
        float px, py, pt, ap = 0;
        float cz = 75;
        float cx, cy;
        int centerX, centerY;
        float ay = 80;

        //Map seeds
        int seedx;
        //Map Dimentions
        const int cubenumbery = 5; //depth from sea (y)
        //number of chunks, must be a root number
        //Will set size of map
        static int width = 16;
        static int length = 16;
        static int chunks = 625;
        static double xychunks = Math.Sqrt((double)chunks);

        //Cube list / render array
        private StaticVBRenderer[,][] staticVBRenderer;
        private List<Vector3>[,] Grass;
        private List<Vector3>[,] Water;
        private List<Vector3>[,] Dirt;
        private List<Vector3>[,] Stone;

        // Content     
        BasicEffect cubeEffect;
        Texture2D[] Textures = new Texture2D[4];
        BoundingBox[,] thisqube = new BoundingBox[(int)xychunks, (int)xychunks];

        // Position related variables
        Vector3 cameraPosition = new Vector3(0, 3, 4);
        Vector3 modelPosition = Vector3.Zero;

        // Misc Declarations
        float rotation = 0.0f;
        Random r = new Random();
        float aspectRatio = 0.0f;

        #endregion

        public Version1()
        {
            // how many seconds do you want the transition to last for?
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(1);

        }

        public override void Initialize()
        {
            // initiate the viewport
            ContentManager Content = ScreenManager.Game.Content;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            staticVBRenderer = new StaticVBRenderer[(int)xychunks, (int)xychunks][];

            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            
            //Textures
            kootenayFont = Content.Load<SpriteFont>("Fonts\\Kootenay");
            staticVBEffect = Content.Load<Effect>("VertexBuffer");
            Textures[0] = Content.Load<Texture2D>("Textures\\CubeTextures\\Stone");
            Textures[1] = Content.Load<Texture2D>("Textures\\CubeTextures\\Grass");
            Textures[2] = Content.Load<Texture2D>("Textures\\CubeTextures\\dirt");
            Textures[3] = Content.Load<Texture2D>("Textures\\CubeTextures\\water");
          
            //Call map generation, at bottom
            GenerateMap();

            base.Initialize();
        }

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(ScreenManager.Game.GraphicsDevice);

            aspectRatio = ScreenManager.Game.GraphicsDevice.Viewport.AspectRatio;
            cubeEffect = new BasicEffect(ScreenManager.Game.GraphicsDevice);

            //Enable textures
            cubeEffect.TextureEnabled = true;

            // Set the World matrix which defines the position of the cube
            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(modelPosition);
            cubeEffect.LightingEnabled = true;
            cubeEffect.DirectionalLight0.Enabled = true;
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime, bool covered)
        {
            InputManager input = ScreenManager.InputSystem;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > 1000.0f)
            {
                fps = totalFrames;
                totalFrames = 0;
                elapsedTime = 0;
            }

            //Exit Key
            KeyboardState keyboard = Keyboard.GetState();
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.Escape))
            {
                Remove();
                ScreenManager.AddScreen(new MainMenu());
            }

            //Camera rotations >.<
            cx = px + (float)Math.Sin(pt) * 5.0f;
            cy = py + (float)Math.Cos(pt) * 5.0f;

            #region Mouse

            //Mouse values/Center
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            centerX = ScreenManager.Game.Window.ClientBounds.Width / 2;
            centerY = ScreenManager.Game.Window.ClientBounds.Height / 2;
            Mouse.SetPosition(centerX, centerY);

            //Mouse functions
            if (mouseState.X < centerX)
            {
                pt += 0.07f;
            }

            if (mouseState.X > centerX)
            {
                pt -= 0.07f;
            }

            if (mouseState.Y < centerY)
            {
                cz += 0.1f;
            }

            if (mouseState.Y > centerY)
            {
                cz -= 0.1f;
            }
            #endregion

            #region Keyboard
            //Keyboard movements

            if (keyState.IsKeyDown(Keys.Add))
            {
                ay -= 0.1f;
                ap -= 0.1f;
                cz -= 0.1f;
            }

            if (keyState.IsKeyDown(Keys.Subtract))
            {
                ay += 0.1f;
                ap += 0.1f;
                cz += 0.1f;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                px += (float)Math.Sin(pt) * 0.3f;
                py += (float)Math.Cos(pt) * 0.3f;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                px += (float)Math.Sin(pt) * -0.3f;
                py += (float)Math.Cos(pt) * -0.3f;
            }

            #endregion

            // Only Update the game when the game is UNPAUSED
            base.Update(gameTime, covered);

            //Pause Key
            if (TotalElapsedTime >= KeyPressCheckDelay)
            {
                if (input.MenuCancel)
                {
                    GameState = GameStates.Paused;
                }
            }
            
            else
            {
                //***********************LOGIC FOR Pause HERE************************
                //UnPause
                if (TotalElapsedTime >= KeyPressCheckDelay)
                {
                    // UnPause the current game
                    if (input.MenuCancel || input.MenuSelect)
                    {

                    }
                }
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

            //Draw when UNPAUSED
            if (GameState == GameStates.Normal)
            {
                totalFrames++;
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

                    cubeEffect.View = Matrix.CreateLookAt(new Vector3(px, ay, py), new Vector3(cx, cz, cy), Vector3.Up);
                    // Set the Projection matrix which defines how we see the scene (Field of view)
                    ScreenManager.Game.GraphicsDevice.BlendState = BlendState.Opaque;
                    ScreenManager.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                    cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);
                    DrawCubes();
                    
                spriteBatch.End();

                displayFPS(gameTime);
            }
            //DRAW WHEN PAUSED
            else if (GameState == GameStates.Paused)
            {
                totalFrames++;
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
                
                spriteBatch.End();
            }
            drawn = 0; //Reset Draws
        }

        public void displayFPS(GameTime gameTime)
        {
            spriteBatch.Begin();
                //Draw FPS
                string FPS = fps.ToString();
                spriteBatch.DrawString(kootenayFont, fpsText, fpsTextLocation, Color.Red);
                spriteBatch.DrawString(kootenayFont, FPS, fpsLocation, Color.Red);
                //Draw numOfDraws
                string numDraws = drawn.ToString();
                spriteBatch.DrawString(kootenayFont, drawsText, drawsTextLocation, Color.Red);
                spriteBatch.DrawString(kootenayFont, numDraws, drawsLocation, Color.Red);
            spriteBatch.End();
        }

        public void DrawCubes()
        {
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                for (byte chunkx = 0; chunkx < xychunks; chunkx++)
                {
                    for (byte chunkz = 0; chunkz < xychunks; chunkz++)
                    {
                        for (int terraintype = 0; terraintype < 4; terraintype++)
                        {
                            if (terraintype == 0)
                                currentVBRenderer = this.staticVBRenderer[chunkx, chunkz][0];
                            if (terraintype == 1)
                                currentVBRenderer = this.staticVBRenderer[chunkx, chunkz][1];
                            if (terraintype == 2)
                                currentVBRenderer = this.staticVBRenderer[chunkx, chunkz][2];
                            if (terraintype == 3)
                                currentVBRenderer = this.staticVBRenderer[chunkx, chunkz][3];
                            if (currentVBRenderer == null)
                                continue;

                            //Render that buffer
                            currentVBRenderer.Render(cubeEffect.View, cubeEffect.Projection, Textures[terraintype]);
                            drawn++;
                        }
                    }
                }
            }
        }

        public void GenerateMap()
        {
            //Make seeds
            seedx = r.Next(60000);
            //Lists for cube storage
            Grass = new List<Vector3>[(int)xychunks, (int)xychunks];
            Dirt = new List<Vector3>[(int)xychunks, (int)xychunks];
            Water = new List<Vector3>[(int)xychunks, (int)xychunks];
            Stone = new List<Vector3>[(int)xychunks, (int)xychunks];
            //Initialize lists, buffer
            for (byte initx = 0; initx < xychunks; initx++)
            {
                for (byte inity = 0; inity < xychunks; inity++)
                {
                    Grass[initx, inity] = new List<Vector3>();
                    Dirt[initx, inity] = new List<Vector3>();
                    Water[initx, inity] = new List<Vector3>();
                    Stone[initx, inity] = new List<Vector3>();
                    staticVBRenderer[initx, inity] = new StaticVBRenderer[4];
                }
            }
            //Generate map in chunks
            for (byte chunkx = 0; chunkx < xychunks; chunkx++)
            {
                for (byte chunkz = 0; chunkz < xychunks; chunkz++)
                {
                    for (int x = 0 + (16 * (int)chunkx); x < width + (16 * (int)chunkx); x++)
                    {
                        for (int z = 0 + (16 * (int)chunkz); z < length + (16 * (int)chunkz); z++)
                        {
                            //perlin noise
                            float octave1 = PerlinNoise.noise((x + seedx) * 0.0001f, (z + seedx) * 0.0001f) * 0f;
                            float octave2 = PerlinNoise.noise((x + seedx) * 0.0005f, (z + seedx) * 0.0005f) * 0f;
                            float octave3 = PerlinNoise.noise((x + seedx) * 0.005f, (z + seedx) * 0.005f) * 0f;
                            float octave4 = PerlinNoise.noise((x + seedx) * 0.01f, (z + seedx) * 0.01f) * 20f;
                            float octave5 = PerlinNoise.noise((x + seedx) * 0.03f, (z + seedx) * 0.03f) * 5f;
                            //15,10 | 30, 8 <----- Octave 4/5 combos that are good so far

                            float lowerGroundHeight = octave1 + octave2 + octave3 + octave4 + octave5;
                            if ((55 + (int)lowerGroundHeight) <= 64) //Check if water
                            {
                                Water[chunkx, chunkz].Add(new Vector3(x, 56 + (int)lowerGroundHeight, z)); //Add to water list
                                if (54 + (int)lowerGroundHeight < 64)
                                {
                                    for (int tosea = (54 + (int)lowerGroundHeight); tosea < 65; tosea++) //Create lakes/oceans depth
                                    {
                                        Water[chunkx, chunkz].Add(new Vector3(x, tosea + 1, z)); //Also add to list
                                    }
                                }
                            }
                            else
                            {
                                Grass[chunkx, chunkz].Add(new Vector3(x, 55 + (int)lowerGroundHeight, z)); //Add grass to list
                            }

                            //Fill in under ground level
                            for (int tobottom = (54 + (int)lowerGroundHeight); tobottom > (54 + (int)lowerGroundHeight) - 3; tobottom--)
                            {
                                if (r.Next(100) > 50)
                                {
                                    Dirt[chunkx, chunkz].Add(new Vector3(x, tobottom, z)); //Add to list
                                }
                                else
                                {
                                    Stone[chunkx, chunkz].Add(new Vector3(x, tobottom, z)); //Add to list
                                }
                            }
                        }
                    }
                }
            }
            //load the buffers
            for (byte chunkx = 0; chunkx < xychunks; chunkx++)
            {
                for (byte chunkz = 0; chunkz < xychunks; chunkz++)
                {
                    //Create buffers in chunks for rendering
                    if (Stone[chunkx, chunkz].Count != 0)
                        this.staticVBRenderer[chunkx, chunkz][0] = new StaticVBRenderer(ScreenManager.Game.GraphicsDevice, Textures[0], Stone[chunkx, chunkz], staticVBEffect);
                    if (Grass[chunkx, chunkz].Count != 0)
                        this.staticVBRenderer[chunkx, chunkz][1] = new StaticVBRenderer(ScreenManager.Game.GraphicsDevice, Textures[1], Grass[chunkx, chunkz], staticVBEffect);
                    if (Dirt[chunkx, chunkz].Count != 0)
                        this.staticVBRenderer[chunkx, chunkz][2] = new StaticVBRenderer(ScreenManager.Game.GraphicsDevice, Textures[2], Dirt[chunkx, chunkz], staticVBEffect);
                    if (Water[chunkx, chunkz].Count != 0)
                        this.staticVBRenderer[chunkx, chunkz][3] = new StaticVBRenderer(ScreenManager.Game.GraphicsDevice, Textures[3], Water[chunkx, chunkz], staticVBEffect);
                }
            }
        }
    }
}
