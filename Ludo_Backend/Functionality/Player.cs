using System.Collections.Generic;
using System.Security.Policy;

namespace Ludo_Backend.Functionaity
{
    public class Player
    {
        public enum PlayerColor
        {
            Blue,
            Yellow,
            Green,
            Red
        }

        public static PlayerColor StringToPlayerColor(string color)
        {
            switch (color)
            {
                case "Blue":
                    return PlayerColor.Blue;
                case "Yellow":
                    return PlayerColor.Yellow;
                case "Red":
                    return PlayerColor.Red; ;
                default:
                    return PlayerColor.Green;
            }
            
        }

        public Player()
        {
            Pawns = new List<Pawn>();
            Score = 0;
        }

        public string Name { get; set; }
        public byte StartPosition { get; set; }
        public byte EndPosition { get; set; }
        public List<Pawn> Pawns { get; set; }
        public PlayerColor Color { get; set; }
        public byte Score { get; set; }
    }
}
