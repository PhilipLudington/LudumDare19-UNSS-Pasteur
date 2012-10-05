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

namespace LD19
{
    public static class Camera
    {
        #region Declarations
        private static Vector2 position = Vector2.Zero;
        private static Vector2 viewPortSize = Vector2.Zero;
        private static Rectangle worldRectangle = new Rectangle(0, 0, 0,0);
        #endregion

        #region Properties
        public static Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = new Vector2(MathHelper.Clamp(value.X,
                    worldRectangle.X,
                    worldRectangle.Width - ViewPortWidth),
                    MathHelper.Clamp(value.Y,
                    worldRectangle.Y,
                    worldRectangle.Height - ViewPortHeight));
            }
        }
        public static Rectangle WorldRectangle
        {
            get
            {
                return worldRectangle;
            }
            set
            {
                worldRectangle = value;
            }
        }
        public static int ViewPortWidth
        {
            get
            {
                return (int)viewPortSize.X;
            }
            set
            {
                viewPortSize.X = value;
            }
        }
        public static int ViewPortHeight
        {
            get
            {
                return (int)viewPortSize.Y;
            }
            set
            {
                viewPortSize.Y = value;
            }
        }
        public static Rectangle ViewPort
        {
            get
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y,
                    ViewPortWidth, ViewPortHeight);
            }
        }
        #endregion

        #region Public Methods
        public static void Move(Vector2 offset)
        {
            TileMap.IsOnScreenTileListInvalidated = true;
            Position += offset;
        }
        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return (ViewPort.Intersects(bounds));
        }
        public static Vector2 Transform(Vector2 point)
        {
            return point - position;
        }
        public static Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle(
                rectangle.Left - (int)position.X,
                rectangle.Top - (int)position.Y,
                rectangle.Width,
                rectangle.Height);
        }
        #endregion
    }
}
