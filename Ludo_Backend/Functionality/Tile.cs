using System.Collections.Generic;

namespace Ludo_Backend.Functionaity
{
    public class Tile
    {
        public byte BoardPosition { get; set; }
        public List<Pawn> CurrentOwnerPawns { get; set; }
        public bool IsSafezone { get; set; }
    }
}