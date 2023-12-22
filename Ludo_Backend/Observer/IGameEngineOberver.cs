using Ludo_Backend.Functionaity;

namespace Ludo_Backend.Observer
{
    public interface IGameEngineOberver
    {
        void NotifyDiceRolled(byte diceValue);
        void NotifySolvePawnCollision(byte pawnPosition);
        void NotifyPawnReleasedFromBase(Player.PlayerColor color, byte newPos);
        void NotifyInPlayPawnMoveMade(byte pawnPosition, byte steps);

    }
}
