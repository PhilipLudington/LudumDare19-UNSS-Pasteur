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

namespace LD19
{
    public class EarthTile : SpaceTile
    {
        public SpaceTile tester;

        public EarthTile(Vector2 worldLocation)
        {
            TextureLink textureLink = TextureManager.Textures[TextureNames.Earth1];

            BaseSprite = new Sprite(worldLocation,
                textureLink.SpriteSheet,
                textureLink.SourceRectangle,
                Vector2.Zero);

            BaseSprite.AnimateWhenStopped = true;
            BaseSprite.CollisionRadius = EncounterRange;

            Encounter = false;
            LongRangeScanned = true;

            tester = new HelpfulScanners(worldLocation);
        }

        public override string GetEncounterText()
        {
            return tester.GetEncounterText();
        }

        public override void EncounterResult(bool action1Selected)
        {
            tester.EncounterResult(action1Selected);
        }
        public override string GetAction1Text()
        {
            return tester.GetAction1Text();
        }
        public override string GetAction2Text()
        {
            return tester.GetAction2Text();
        }
        public override bool IsAction2()
        {
            return tester.IsAction2();
        }
    }
}
