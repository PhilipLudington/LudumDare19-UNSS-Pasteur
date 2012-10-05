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
using System;

namespace LD19
{
    public class SpaceTile
    {
        public Sprite BaseSprite;
        public bool Encounter = false;
        public int EncounterRange = 4;

        public bool IsStar
        {
            get;
            set;
        }            
        
        public int UnknownTileIndex
        {
            get;
            set;
        }

        public int LongRangeScannedTileIndex
        {
            get;
            set;
        }

        public bool LongRangeScanned = false;

        public SpaceTile(Vector2 worldLocation)
        {
            TextureNames textureName = (TextureNames)Random.Next((int)TextureNames.Unknown1, (int)TextureNames.Unknown6);
            TextureLink textureLink = TextureManager.Textures[textureName];

            BaseSprite = new Sprite(worldLocation,
                textureLink.SpriteSheet,
                textureLink.SourceRectangle,
                Vector2.Zero);

            BaseSprite.AnimateWhenStopped = true;
            BaseSprite.CollisionRadius = EncounterRange;

            IsStar = false;
        }
        public SpaceTile()        
        {
            IsStar = false;
        }

        public int GetTileIndex()
        {
            if (LongRangeScanned)
            {
                return LongRangeScannedTileIndex;
            }
            else
            {
                return UnknownTileIndex;
            }
        }

        public virtual void LongRangeScan()
        {
            if (LongRangeScanned == false)
            {
                LongRangeScanned = true; 
                
                TextureNames textureName = (TextureNames)Random.Next((int)TextureNames.Empty1, (int)TextureNames.Empty4);
                TextureLink textureLink = TextureManager.Textures[textureName];

                BaseSprite = new Sprite(BaseSprite.WorldLocation,
                    textureLink.SpriteSheet,
                    textureLink.SourceRectangle,
                    Vector2.Zero);

                BaseSprite.AnimateWhenStopped = true;
                BaseSprite.CollisionRadius = EncounterRange;
            }
        }

        public virtual string GetEncounterText()
        {
            return "You find nothing... maybe it was a scanner glitch?";
        }

        public virtual void EncounterResult(bool action1Selected)
        {
            EncounterManager.EncounterActive = false;

            LongRangeScanned = true;

            TextureNames textureName = (TextureNames)Random.Next((int)TextureNames.Empty1, (int)TextureNames.Empty4);
            TextureLink textureLink = TextureManager.Textures[textureName];

            BaseSprite = new Sprite(BaseSprite.WorldLocation,
                textureLink.SpriteSheet,
                textureLink.SourceRectangle,
                Vector2.Zero);

            BaseSprite.AnimateWhenStopped = true;
            BaseSprite.CollisionRadius = 1;

            Encounter = false;
        }
        public virtual string GetAction1Text()
        {
            return "Strange, we better move on.";
        }
        public virtual string GetAction2Text()
        {
            return "";
        }
        public virtual bool IsAction2()
        {
            return false;
        }
    }
}
