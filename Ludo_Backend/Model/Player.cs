using System.Collections.Generic;

namespace Ludo_Backend.Model
{
    public class Player
    {
        public enum PlayerColor
        {
            Blue,
            Green,
            Red,
            Yellow,
        }

        public string Name { get; set; }
        public byte StartPosition { get; set; }
        public byte EndPosition { get; set; }
        public List<Pawn> Pawns { get; set; }
        public PlayerColor Color { get; set; }
    }
}
