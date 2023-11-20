namespace Ludo.Model
{
    public class Tile
    {
        public byte BoardPosition { get; set; }
        public Pawn CurrentOwner { get; set; }
        public bool IsSafezone { get; set; }
    }
}
