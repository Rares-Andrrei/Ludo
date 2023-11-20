namespace Ludo.Model
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
        public byte RemainingPawns { get; set; }
        public PlayerColor Color { get; set; }
    }
}
