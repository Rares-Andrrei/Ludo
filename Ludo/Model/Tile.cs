using System.Collections.Generic;

namespace Ludo.Model
{
    public class Tile
    {
        public byte BoardPosition { get; set; }
        public List<Pawn> CurrentOwnerPawns { get; set; }
        public bool IsSafezone { get; set; }
    }
}