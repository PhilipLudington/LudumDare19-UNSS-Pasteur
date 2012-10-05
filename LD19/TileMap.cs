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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD19
{
    public static class TileMap
    {
        #region Declarations
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        public const int MapWidth = 1000;
        public const int MapHeight = 1000;

        public const int EmptySpaceTileStart = 0;
        public const int EmptySpaceTileEnd = 3;
        public const int WallTileStart = 4;
        public const int WallTileEnd = 7;
        public const int UnknownTileStart = 8;
        public const int UnknownTileEnd = 13;
        public const int StarTileStart = 14;
        public const int StarTileEnd = 19;
        public const int StarTileBuffer = 5;

        public static bool IsOnScreenTileListInvalidated = true;
        private static List<SpaceTile> onScreenTileList = new List<SpaceTile>();

        static private Texture2D texture;        
        static private List<Rectangle> tiles = new List<Rectangle>();
        static private SpaceTile[,] mapSquares = new SpaceTile[MapWidth, MapHeight];
        #endregion

        #region Initialization
        static public void Initialize(Texture2D tileTexture)
        {
            texture = tileTexture;
            tiles.Clear();

            // Empty Space Tiles
            tiles.Add(new Rectangle(0, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 0, TileWidth, TileHeight));

            // Wall Tiles
            tiles.Add(new Rectangle(0, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 32, TileWidth, TileHeight));

            // ShipTiles
            //tiles.Add(new Rectangle(0, 64, TileWidth, TileHeight));
            //tiles.Add(new Rectangle(32, 64, TileWidth, TileHeight));
            //tiles.Add(new Rectangle(64, 64, TileWidth, TileHeight));
            //tiles.Add(new Rectangle(96, 64, TileWidth, TileHeight));
            //tiles.Add(new Rectangle(128, 64, TileWidth, TileHeight));
            //tiles.Add(new Rectangle(160, 64, TileWidth, TileHeight));
            
            // Unknown Tiles
            tiles.Add(new Rectangle(0, 96, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 96, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 96, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 96, TileWidth, TileHeight));
            tiles.Add(new Rectangle(128, 96, TileWidth, TileHeight));
            tiles.Add(new Rectangle(160, 96, TileWidth, TileHeight));

            // StarTiles
            tiles.Add(new Rectangle(0, 128, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 128, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 128, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 128, TileWidth, TileHeight));
            tiles.Add(new Rectangle(128, 128, TileWidth, TileHeight));
            tiles.Add(new Rectangle(160, 96, TileWidth, TileHeight));

            //for (int x = 0; x < MapWidth; x++)
            //    for (int y = 0; y < MapHeight; y++)
            //    {
            //        mapSquares[x, y] = FloorTileStart;
            //    }

            GenerateRandomMap();
        }
        #endregion

        #region Information about Map Squares

        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        static public Vector2 GetSquareAtPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static public Vector2 GetSquareCenter(int squareX, int squareY)
        {
            return new Vector2(
                (squareX * TileWidth) + (TileWidth / 2),
                (squareY * TileHeight) + (TileHeight / 2));
        }

        static public Vector2 GetSquareCenter(Vector2 square)
        {
            return GetSquareCenter((int)square.X, (int)square.Y);
        }

        static public Rectangle SquareWorldRectangle(int x, int y)
        {
            return new Rectangle(
                x * TileWidth,
                y * TileHeight,
                TileWidth,
                TileHeight);
        }

        static public Rectangle SquareWorldRectangle(Vector2 square)
        {
            return SquareWorldRectangle(
                (int)square.X,
                (int)square.Y);
        }

        static public Rectangle SquareScreenRectangle(int x, int y)
        {
            return Camera.Transform(SquareWorldRectangle(x, y));
        }

        static public Rectangle SquareScreenRectangle(Vector2 square)
        {
            return SquareScreenRectangle((int)square.X, (int)square.Y);
        }
        #endregion

        #region Information about Map Tiles
        static public int GetTileAtSquare(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth)
                && (tileY >= 0) && (tileY < MapHeight))
            {
                return mapSquares[tileX, tileY].GetTileIndex();
            }
            else
            {
                return -1;
            }
        }
        static public SpaceTile GetSpaceTileAtPixel( int x, int y )
        {
            if (x > 0 && y > 0 && x < MapWidth * TileWidth && y < MapHeight * TileHeight )
            {
                return mapSquares[GetSquareByPixelX(x),
                GetSquareByPixelY(y)];
            }
            else
            {
                return null;
            }
        }
        //static public void SetTileAtSquare(int tileX, int tileY, int tile)
        //{
        //    if ((tileX >= 0) && (tileX < MapWidth) &&
        //        (tileY >= 0) && (tileY < MapHeight))
        //    {
        //        mapSquares[tileX, tileY] = tile;
        //    }
        //}

        static public int GetTileAtPixel(int pixelX, int pixelY)
        {
            return GetTileAtSquare(
                GetSquareByPixelX(pixelX),
                GetSquareByPixelY(pixelY));
        }

        static public int GetTileAtPixel(Vector2 pixelLocation)
        {
            return GetTileAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        static public bool IsWallTile(int tileX, int tileY)
        {
            int tileIndex = GetTileAtSquare(tileX, tileY);

            if (tileIndex == -1)
            {
                return false;
            }

            return tileIndex >= WallTileStart;
        }

        static public bool IsWallTile(Vector2 square)
        {
            return IsWallTile((int)square.X, (int)square.Y);
        }

        static public bool IsWallTileByPixel(Vector2 pixelLocation)
        {
            return IsWallTile(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }
        #endregion

        public static List<SpaceTile> GetSpaceTilesInView()
        {
            if (IsOnScreenTileListInvalidated)
            {
                onScreenTileList = new List<SpaceTile>();

                int startX = GetSquareByPixelX((int)Camera.Position.X);
                int endX = GetSquareByPixelY((int)Camera.Position.X + Camera.ViewPortWidth);

                int startY = GetSquareByPixelY((int)Camera.Position.Y);
                int endY = GetSquareByPixelY((int)Camera.Position.Y + Camera.ViewPortHeight);

                for (int x = startX; x <= endX; x++)
                    for (int y = startY; y <= endY; y++)
                    {
                        if ((x >= 0) && (y >= 0) &&
                            (x < MapWidth) && (y < MapHeight))
                        {
                            onScreenTileList.Add(mapSquares[x, y]);
                        }
                    }
            }

            return onScreenTileList;
        }
        #region Drawing
        static public void Draw(SpriteBatch spriteBatch)
        {
            List<SpaceTile> onScreen = GetSpaceTilesInView();

            PrimitiveLine encounterRangeBrush = new PrimitiveLine(spriteBatch.GraphicsDevice);
            encounterRangeBrush.Colour = Color.Red;
            foreach( SpaceTile spaceTile in onScreen)
            {
                spaceTile.BaseSprite.Draw(spriteBatch);
                
                // Draw Bounding Cirles
                if (false)
                {
                    encounterRangeBrush.CreateCircle(spaceTile.EncounterRange, 12);
                    encounterRangeBrush.Position = spaceTile.BaseSprite.ScreenCenter;
                    encounterRangeBrush.Render(spriteBatch);
                }
            }

            //int startX = GetSquareByPixelX((int)Camera.Position.X);
            //int endX = GetSquareByPixelY((int)Camera.Position.X + Camera.ViewPortWidth);

            //int startY = GetSquareByPixelY((int)Camera.Position.Y);
            //int endY = GetSquareByPixelY((int)Camera.Position.Y + Camera.ViewPortHeight);

            //for (int x = startX; x <= endX; x++)
            //    for (int y = startY; y <= endY; y++)
            //    {
            //        if ((x >= 0) && (y >= 0) &&
            //            (x < MapWidth) && (y < MapHeight))
            //        {                                        
            //            spriteBatch.Draw(
            //                texture,
            //                SquareScreenRectangle(x, y),
            //                tiles[GetTileAtSquare(x, y)],
            //                Color.White);
            //        }
            //    }
        }
        #endregion

        #region Map Generation
        static public void GenerateRandomMap()
        {
            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                {
                    //mapSquares[x, y] = floorTile;

                    //if ((x == 0) || (y == 0) ||
                    //    (x == MapWidth - 1) || (y == MapHeight - 1))
                    //{
                    //    mapSquares[x, y] = wallTile;
                    //    continue;
                    //}

                    //if ((x == 1) || (y == 1) ||
                    //    (x == MapWidth - 2) || (y == MapHeight - 2))
                    //{
                    //    continue;
                    //}

                    int roll = Random.Next(0, 100);
                    int unknownTileIndex = Random.Next(UnknownTileStart, UnknownTileEnd + 1);
                    if (roll < 10)
                    {
                        // Star
                        // Is there a star too close?
                        bool empty = true;
                        for (int i = x - StarTileBuffer; i < x + StarTileBuffer; i++)
                            for (int k = y - StarTileBuffer; k < y + StarTileBuffer; k++)
                            {
                                if (i < 0 || k < 0 ||
                                    i >= MapWidth || k >= MapHeight ||
                                    mapSquares[i, k] == null)
                                {
                                    // Do nothing
                                }
                                else if (mapSquares[i, k].IsStar)
                                {
                                    empty = false;
                                    break;
                                }
                            }

                        if (empty)
                        {
                            Vector2 position = new Vector2(SquareWorldRectangle(x, y).X,
                                    SquareWorldRectangle(x, y).Y);
                            roll = Random.Next(1, 5);
                            switch( roll )
                            {
                                case 1:
                                    mapSquares[x,y] = new HelpfulScanners(position);
                                    break;
                                case 2:
                                    mapSquares[x, y] = new KilledWorldTile(position);
                                    break;
                                case 3:
                                    mapSquares[x, y] = new ScaredWorldTile(position);
                                    break;
                                case 4:
                                    mapSquares[x, y] = new EmptyStar(position);
                                    break;
                                default:
                                    mapSquares[x, y] = new StarTile(position);
                                    break;
                            }
                        }
                        else
                        {
                            // Empty Space
                            mapSquares[x, y] = new SpaceTile(new Vector2(SquareWorldRectangle(x, y).X,
                            SquareWorldRectangle(x, y).Y));
                        }
                    }
                    else
                    {
                        // Empty Space
                        mapSquares[x, y] = new SpaceTile(new Vector2(SquareWorldRectangle(x, y).X,
                            SquareWorldRectangle(x, y).Y));
                    }
                }

            // Find star near starting point
            // and replace with Earth
            Vector2 playerStart = GetSquareAtPixel(Player.BaseSprite.WorldLocation);
            bool stop = false;
            for (int x = (int)playerStart.X - StarTileBuffer; x < playerStart.X + StarTileBuffer; x++)
            {
                if (stop)
                {
                    break;
                }
                for (int y = (int)playerStart.Y - StarTileBuffer; y < playerStart.Y + StarTileBuffer; y++)
                {
                    if (mapSquares[x, y].IsStar)
                    {
                        mapSquares[x, y] = new EarthTile(new Vector2( SquareWorldRectangle(x, y).X, SquareWorldRectangle(x,y ).Y));
                        stop = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
