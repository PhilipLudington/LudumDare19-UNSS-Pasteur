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
    class ScaredWorldTile : StarTile
    {
        public ScaredWorldTile(Vector2 worldLocation)
            : base(worldLocation)
        {
        }

        public override string GetEncounterText()
        {
            return "ANY ATTEMPT TO LAND ON OUR PLANET WILL BE MET WITH HOSTILITY!\r\nLEAVE IMMEDIATELY!\r\nDo you try to land a small scouting party in secret?";
        }

        public override void EncounterResult(bool action1Selected)
        {
            if (action1Selected)
            {
                int roll = Random.Next(1, 30);

                string message = "The scout ship hits a space mine and is discovered!\r\nThe planet hits the ship with several nuclear weapons!\r\nRadiation kills most of the crew.\r\nYou have failed.\r\nHumanity devolves to is caveman roots.";
                Game1.Failure = true;
                FadeMessageManager.Show(message);
            }

            EncounterManager.EncounterActive = false;

            BaseSprite.TintColor = Color.Gray;

            Encounter = false;
        }
        public override string GetAction1Text()
        {
            return "Yes, we need more information.";
        }
        public override string GetAction2Text()
        {
            return "No violence thanks.";
        }
        public override bool IsAction2()
        {
            return true;
        }
    }
}
