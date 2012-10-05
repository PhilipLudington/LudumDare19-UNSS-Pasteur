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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD19
{
    static public class Player
    {
        #region Declarations
        public static Sprite BaseSprite;
        public static int LongRange = 190;
        public static int EncounterRange = 16;
        private static Rectangle scrollArea = new Rectangle(375, 275, 50, 50);
        private static Vector2 baseAngle = Vector2.Zero;
        public static float PlayerSpeed = 90f;
        #endregion

        #region Initialization
        static public void Initialize(
            Texture2D texture,
            Rectangle baseInitialFrame,
            int baseFrameCount,
            Vector2 worldLocation)
        {
            int frameWidth = baseInitialFrame.Width;
            int frameHeight = baseInitialFrame.Height;

            BaseSprite = new Sprite(worldLocation,
                texture,
                baseInitialFrame,
                Vector2.Zero);

            BaseSprite.BoundingXPadding = 4;
            BaseSprite.BoundingYPadding = 4;
            BaseSprite.AnimateWhenStopped = false;

            for (int x = 1; x < baseFrameCount; x++)
            {
                BaseSprite.AddFrame(
                    new Rectangle(
                        baseInitialFrame.X + (frameHeight * x),
                        baseInitialFrame.Y,
                        frameWidth,
                        frameHeight));
            }
        }
        #endregion

        #region Update and Draw
        public static void Update(GameTime gameTime)
        {
            HandleInput(gameTime);            
            BaseSprite.Update(gameTime);
            ClampToWorld();

            // Reveal unexplored tiles (widest scan)
            List<SpaceTile> near = TileMap.GetSpaceTilesInView();

            SpaceTile encounter = null;

            foreach (SpaceTile spaceTile in near)
            {
                if (spaceTile.BaseSprite.IsCircleColliding(Player.BaseSprite.WorldCenter, Player.LongRange))
                {
                    spaceTile.LongRangeScan();
                }

                if (EncounterManager.EncounterActive)
                {
                    // Do Nothing
                }
                else
                {
                    if (spaceTile.BaseSprite.IsCircleColliding(BaseSprite.WorldCenter, EncounterRange))
                    {
                        if (encounter == null)
                        {
                            encounter = spaceTile;
                        }
                        else
                        {
                            if (Vector2.Distance(spaceTile.BaseSprite.WorldCenter, Player.BaseSprite.WorldCenter) >
                                Vector2.Distance(encounter.BaseSprite.WorldCenter, Player.BaseSprite.WorldCenter))
                            {
                                encounter = spaceTile;
                            }
                        }
                    }
                }
            }

            EncounterManager.Start(encounter);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            BaseSprite.Draw(spriteBatch);

            // Draw Bounding Cirles
            bool drawBoundingCirles = false;
            if (drawBoundingCirles)
            {
                PrimitiveLine brush = new PrimitiveLine(spriteBatch.GraphicsDevice);
                brush.Colour = Color.Green;
                brush.CreateCircle(LongRange, 42);
                brush.Position = BaseSprite.ScreenCenter;
                brush.Render(spriteBatch);

                PrimitiveLine encounterRangeBrush = new PrimitiveLine(spriteBatch.GraphicsDevice);
                encounterRangeBrush.Colour = Color.Green;
                encounterRangeBrush.CreateCircle(EncounterRange, 42);
                encounterRangeBrush.Position = BaseSprite.ScreenCenter;
                encounterRangeBrush.Render(spriteBatch);
            }
        }
        #endregion

        #region Input Handling
        private static Vector2 HandleKeyboardMovement(KeyboardState keyState)
        {
            Vector2 keyMovement = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                keyMovement.Y--;

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                keyMovement.X--;

            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                keyMovement.Y++;

            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                keyMovement.X++;

            return keyMovement;
        }

        private static void HandleInput(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveAngle = Vector2.Zero;
            
            if (EncounterManager.EncounterActive)
            {
                // Do not handle input
            }
            else
            {
                moveAngle += HandleKeyboardMovement(Keyboard.GetState());
            }

            if (moveAngle != Vector2.Zero)
            {
                moveAngle.Normalize();
                baseAngle = moveAngle;
            }

            BaseSprite.RotateTo(baseAngle);

            BaseSprite.Velocity = moveAngle * PlayerSpeed;

            repositionCamera(gameTime, moveAngle);
        }
        #endregion

        #region Movement Limitations
        private static void ClampToWorld()
        {
            float currentX = BaseSprite.WorldLocation.X;
            float currentY = BaseSprite.WorldLocation.Y;

            currentX = MathHelper.Clamp(
                currentX,
                0,
                Camera.WorldRectangle.Right - BaseSprite.FrameWidth);

            currentY = MathHelper.Clamp(
                currentY,
                0,
                Camera.WorldRectangle.Bottom - BaseSprite.FrameHeight);

            BaseSprite.WorldLocation = new Vector2(currentX, currentY);
        }
        private static void repositionCamera(
            GameTime gameTime,
            Vector2 moveAngle )
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float moveScale = PlayerSpeed * elapsed;

            if((BaseSprite.ScreenRectangle.X < scrollArea.X) &&
                (moveAngle.X < 0 ))
            {
                Camera.Move(new Vector2(moveAngle.X,0) * moveScale);
            }

            if((BaseSprite.ScreenRectangle.Right > scrollArea.Right) &&
                (moveAngle.X > 0 ))
            {
                Camera.Move(new Vector2(moveAngle.X,0) * moveScale );
            }

            if((BaseSprite.ScreenRectangle.Y < scrollArea.Y) &&
                (moveAngle.Y < 0))
            {
                Camera.Move( new Vector2( 0, moveAngle.Y) * moveScale );
            }

            if((BaseSprite.ScreenRectangle.Bottom > scrollArea.Bottom) &&
                (moveAngle.Y > 0))
            {
                Camera.Move( new Vector2( 0, moveAngle.Y) * moveScale );
            }
        }
        #endregion

    }
}
