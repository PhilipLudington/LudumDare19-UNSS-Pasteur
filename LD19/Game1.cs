#region Copyright Notice and Lisenses
/*
    Copyright 2011 Philip Ludington
 
    This file is part of LD19.

    LD19 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LD19 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LD19.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD19
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        Texture2D titleScreen;
        SpriteFont pericles;
        public static bool Failure = false;
        public static bool Success = false;
        public static bool Title = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteSheet = Content.Load<Texture2D>(@"Textures\SpriteSheet");
            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
            pericles = Content.Load<SpriteFont>(@"Fonts\Pericles");

            SoundManager.Initialize(Content);
            TextureManager.Initialize(Content, spriteSheet);
            EncounterManager.Initialize(pericles);
            FadeMessageManager.Initialize(pericles);
        }

        private void RestartGame()
        {
            int startX = Random.Next(Convert.ToInt32(TileMap.MapWidth * 0.4), Convert.ToInt32(TileMap.MapWidth * .6));
            int startY = Random.Next(Convert.ToInt32(TileMap.MapHeight * 0.4), Convert.ToInt32(TileMap.MapHeight * .6));
            Player.Initialize(
                spriteSheet,
                new Rectangle(0, 64, 32, 32),
                6,
                new Vector2(startX * TileMap.TileWidth, startY * TileMap.TileHeight)
                );


            Camera.WorldRectangle = new Rectangle(0, 0, TileMap.MapWidth * TileMap.TileWidth, TileMap.MapHeight * TileMap.TileHeight);
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
            Camera.Position = new Vector2(startX * TileMap.TileWidth - Camera.ViewPortWidth / 2, startY * TileMap.TileHeight - Camera.ViewPortHeight / 2);

            TileMap.Initialize(spriteSheet);

            EncounterManager.EncounterActive = false;
            FadeMessageManager.Show("Good Luck Captain!  All of humanity is with you.");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            if (FrameClear.IsClear < gameTime.TotalGameTime.TotalSeconds)
            {
                if (Title)
                {
                    // Display Title Screen
                    FadeMessageManager.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Title = false;
                        RestartGame();
                    }
                }
                else if (Failure)
                {
                    // Display Failure screen
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Title = true;
                        Failure = false;
                        FrameClear.IsClear = gameTime.TotalGameTime.TotalSeconds + 0.3;
                    }
                }
                else if (Success)
                {
                    // Display Success Screen
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Title = true;
                        Success = false;
                        FrameClear.IsClear = gameTime.TotalGameTime.TotalSeconds + 0.3;
                    }
                }
                else
                {
                    Player.Update(gameTime);

                    EncounterManager.Update(gameTime);
                    FadeMessageManager.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            if (Title)
            {
                // Display Title Screen
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (Failure)
            {
                // Display Failure screen
            }
            else if (Success)
            {
                // Display Success Screen
            }
            else
            {
                TileMap.Draw(spriteBatch);
                Player.Draw(spriteBatch);
                EncounterManager.Draw(spriteBatch);
            }
            FadeMessageManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
