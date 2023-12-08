namespace Ludo.Model
{
    public class Pawn
    {
        public enum PawnState
        {
            InBase,
            InPlay,
            AlmostFinished,
            Finished,
        }
        public byte Position { get; set; }

        private readonly Player owner;
        public Player Owner => owner;

        public PawnState State { get; set; }
    }
}
