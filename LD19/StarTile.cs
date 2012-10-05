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
using Microsoft.Xna.Framework;

namespace LD19
{
    public class StarTile : SpaceTile
    {    
        public StarTile(Vector2 worldLocation)
            : base(worldLocation)
        {
            Encounter = true;
            IsStar = true;
        }

        public override void LongRangeScan()
        {
            if (LongRangeScanned == false)
            {
                SoundManager.CivAlert.Play();

                LongRangeScanned = true;

                TextureNames textureName = (TextureNames)Random.Next((int)TextureNames.Star1, (int)TextureNames.Star7);
                TextureLink textureLink = TextureManager.Textures[textureName];

                BaseSprite = new Sprite(BaseSprite.WorldLocation,
                    textureLink.SpriteSheet,
                    textureLink.SourceRectangle,
                    Vector2.Zero);

                Color tint = new Color( Random.Next(1, 254), Random.Next(1,254), Random.Next(1,254));
                BaseSprite.TintColor = tint;

                BaseSprite.AnimateWhenStopped = true;
                BaseSprite.CollisionRadius = 1;
            }
        }
    }
}
