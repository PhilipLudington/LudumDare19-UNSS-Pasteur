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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LD19
{
    public static class FadeMessageManager
    {
        public static string Message = ".";
        public static SpriteFont Font;
        private static bool showing = false;
        private static double timer;

        public static void Initialize( SpriteFont font )
        {
            Font = font;
        }

        public static void Show(string message)
        {
            Message = message;
            showing = true;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (showing)
            {
                spriteBatch.DrawString(Font, Message, new Vector2(10, 250), Color.White);
            }
        }
        public static void Update(GameTime gameTime)
        {
            if (showing)
            {
                if (timer == -1)
                {
                    // Start the timer
                    timer = gameTime.TotalGameTime.TotalSeconds + 5;
                }
                else if (timer < gameTime.TotalGameTime.TotalSeconds)
                {
                    // Times up
                    showing = false;
                    timer = -1;
                }
            }
        }
    }
}
