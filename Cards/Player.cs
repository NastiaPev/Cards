using System.Collections.Generic;

namespace Cards
{
    public enum Player { North, South, East, West}

    static class PlayerSerialiser
    {

        private static Dictionary<Player, string> PLayerNames = new Dictionary<Player, string> {
            {Player.North, "north"},
            {Player.South, "south"},
            {Player.East, "east"},
            {Player.West, "west"}
        };

        /// <summary>
        /// An XML-suitable name for the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string Name(this Player player)
        {
            return PLayerNames[player];
        }
    }

}