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
    public class EmptyStar : StarTile
    {
        public EmptyStar(Vector2 worldLocation)
            : base(worldLocation)
        {
        }

        public override string GetEncounterText()
        {
            return "You discover nothing of interest";
        }

        public override void EncounterResult(bool action1Selected)
        {
            EncounterManager.EncounterActive = false;

            LongRangeScanned = true;
            
            BaseSprite.TintColor = Color.Gray;

            Encounter = false;
        }
        public override string GetAction1Text()
        {
            return "Oh, well let's get going then.";
        }
        public override string GetAction2Text()
        {
            return "";
        }
        public override bool IsAction2()
        {
            return false;
        }
    }
}
