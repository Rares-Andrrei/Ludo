namespace Ludo_Backend.Model
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

        public Pawn(Player owner)
        {
            State = PawnState.InBase;
            this.owner = owner;
        }

        public byte Position { get; set; }

        private readonly Player owner;
        public Player Owner => owner;

        public PawnState State { get; set; }
    }
}
