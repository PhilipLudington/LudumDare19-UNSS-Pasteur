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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LD19
{
    public enum TextureNames : int
    {
        Earth1,
        Empty1,
        Empty2,
        Empty3,
        Empty4,
        Wall1,
        Wall2,
        Wall3,
        Wall4,
        Ship1,
        Ship2,
        Ship3,
        Ship4,
        Ship5,
        Ship6,
        Unknown1,
        Unknown2,
        Unknown3,
        Unknown4,
        Unknown5,
        Unknown6,
        Star1,
        Star2,
        Star3,
        Star4,
        Star5,
        Star6,
        Star7

    }

    public static class TextureManager
    {
        public static Dictionary<TextureNames, TextureLink> Textures
        {
            get;
            private set;
        }

        #region Initialization
        static public void Initialize(ContentManager content, Texture2D tileTexture)
        {
            Textures = new Dictionary<TextureNames, TextureLink>();

            Texture2D spriteSheet = content.Load<Texture2D>(@"Textures\SpriteSheet");
            int TileWidth = 32;
            int TileHeight = 32;

            // Earth Tile
            AddTexture(TextureNames.Earth1, spriteSheet, 128, 0, TileWidth, TileHeight);

            // Empty Space Tiles
            AddTexture(TextureNames.Empty1, spriteSheet, 0, 0, TileWidth, TileHeight);
            AddTexture(TextureNames.Empty2, spriteSheet, 32, 0, TileWidth, TileHeight);
            AddTexture(TextureNames.Empty3, spriteSheet, 64, 0, TileWidth, TileHeight);
            AddTexture(TextureNames.Empty4, spriteSheet, 96, 0, TileWidth, TileHeight);

            // Wall Tiles
            AddTexture(TextureNames.Wall1, spriteSheet, 0, 32, TileWidth, TileHeight);
            AddTexture(TextureNames.Wall2, spriteSheet, 32, 32, TileWidth, TileHeight);
            AddTexture(TextureNames.Wall3, spriteSheet, 64, 32, TileWidth, TileHeight);
            AddTexture(TextureNames.Wall4, spriteSheet, 96, 32, TileWidth, TileHeight);

            // Ship Tiles
            AddTexture(TextureNames.Ship1, spriteSheet, 0, 64, TileWidth, TileHeight);
            AddTexture(TextureNames.Ship2, spriteSheet, 32, 64, TileWidth, TileHeight);
            AddTexture(TextureNames.Ship3, spriteSheet, 64, 64, TileWidth, TileHeight);
            AddTexture(TextureNames.Ship4, spriteSheet, 96, 64, TileWidth, TileHeight);
            AddTexture(TextureNames.Ship5, spriteSheet, 128, 64, TileWidth, TileHeight);
            AddTexture(TextureNames.Ship6, spriteSheet, 160, 64, TileWidth, TileHeight);

            // Unknown Tiles
            AddTexture(TextureNames.Unknown1, spriteSheet, 0, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Unknown2, spriteSheet, 32, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Unknown3, spriteSheet, 64, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Unknown4, spriteSheet, 96, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Unknown5, spriteSheet, 128, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Unknown6, spriteSheet, 160, 96, TileWidth, TileHeight);

            // Star Tiles
            AddTexture(TextureNames.Star1, spriteSheet, 0, 128, TileWidth, TileHeight);
            AddTexture(TextureNames.Star2, spriteSheet, 32, 128, TileWidth, TileHeight);
            AddTexture(TextureNames.Star3, spriteSheet, 64, 128, TileWidth, TileHeight);
            AddTexture(TextureNames.Star4, spriteSheet, 96, 128, TileWidth, TileHeight);
            AddTexture(TextureNames.Star5, spriteSheet, 128, 128, TileWidth, TileHeight);
            AddTexture(TextureNames.Star6, spriteSheet, 160, 96, TileWidth, TileHeight);
            AddTexture(TextureNames.Star7, spriteSheet, 192, 96, TileWidth, TileHeight);
        }
        public static void AddTexture( TextureNames textureName, Texture2D spriteSheet, int x, int y, int width, int height )
        {
            Textures.Add(textureName, new TextureLink(spriteSheet, x, y, width, height));
        }
        #endregion
    }
}
