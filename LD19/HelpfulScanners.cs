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
    public class HelpfulScanners : StarTile
    {
        public HelpfulScanners(Vector2 worldLocation)
            : base(worldLocation)
        {
        }

        public override string GetEncounterText()
        {
            return "You discover a friendly civilication.\r\nThey can't help with a cure, but they do offer try an improve our ship?\r\nDo you accept there help?";
        }

        public override void EncounterResult(bool action1Selected)
        {
            if (action1Selected)
            {
                int roll = Random.Next(1, 5);

                string message = "They find no way to help, and wish you good luck.";
                switch (roll)
                {
                    case 1:
                        roll = Random.Next(1, 10);
                        message = string.Format("Excellent, scanner range improved by {0}%!", roll);
                        Player.LongRange += (int)(Player.LongRange * roll / 100);
                        break;
                    case 2:
                        roll = Random.Next(9, 26);
                        message = string.Format("Excellent, scanner range improved by {0}%!", roll);
                        Player.LongRange += (int)(Player.LongRange * roll / 100);
                        break;
                    case 3:
                        roll = Random.Next(1, 10);
                        if (roll == 7)
                        {
                            message = "Through and unfortunate accident, the scanner array is damaged, range reduced to 80%!!";
                            Player.LongRange -= (int)(Player.LongRange * 0.80);
                        }
                        break;
                    case 4:                        
                        roll = Random.Next(1, 10);
                        if (roll == 10)
                        {
                            message = "The entire crew has been exposed to an alien deseases and the quarantine failed.\r\nThe entire crew eventually dies.\r\nYou’ve failed and human's are extinct.";
                            Game1.Failure = true;
                        }
                        break;
                    case 5:
                        roll = Random.Next(1, 9);
                        message = string.Format("Engine's improved, speed increased by {0}%!", roll);
                        Player.PlayerSpeed += (int)(Player.PlayerSpeed * roll / 100);
                        break;
                }
                FadeMessageManager.Show(message);
            }

            EncounterManager.EncounterActive = false;

            LongRangeScanned = true;

            TextureNames textureName = (TextureNames)Random.Next((int)TextureNames.Empty1, (int)TextureNames.Empty4);
            TextureLink textureLink = TextureManager.Textures[textureName];

            BaseSprite.TintColor = Color.Gray;

            Encounter = false;
        }
        public override string GetAction1Text()
        {
            return "Yes, we need all the help we can get.";
        }
        public override string GetAction2Text()
        {
            return "No, I don't trust them with access to our ship.";
        }
        public override bool IsAction2()
        {
            return true;
        }
    }
}
