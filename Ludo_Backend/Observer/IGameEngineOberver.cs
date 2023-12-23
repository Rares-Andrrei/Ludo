using Ludo_Backend.Functionaity;

namespace Ludo_Backend.Observer
{
    public interface IGameEngineOberver
    {
        void NotifyDiceRolled(byte diceValue);
        void NotifyPawnReleasedFromBase(Player.PlayerColor color, byte newPos);
        void NotifyScoreCanged(Player.PlayerColor color, byte score);
        void NotifyGameFinished(Player.PlayerColor winner);
        void NotifyInPlayPawnMoveMade(byte pawnPosition, byte destination);
        void NotifyAlmostFinishedPawnMoveMade(Player.PlayerColor playerColor, byte pawnPosition, byte destination);
        void NotifyInPlayToAlmostFinishedPawnMoveMade(Player.PlayerColor playerColor, byte pawnPosition, byte destination);
        void NotifyCollision();
        void NotifyPawnFinished(Pawn.PawnState pawnState, Player.PlayerColor playerColor, byte pawnPosition);
    }
}
