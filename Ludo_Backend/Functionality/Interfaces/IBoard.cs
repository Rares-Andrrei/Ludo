using Ludo_Backend.Observer;
using System.Collections.Generic;

namespace Ludo_Backend.Functionaity.Interfaces
{
    public interface IBoard : IGameEngineSubject
    {
        bool MoveAlmostFinishedPawn(Player.PlayerColor playerColor, byte position, byte steps);
        bool MoveInPlayPawn(byte pawnPosition, byte steps, Player currentPlayer);
        bool IsMoveValid(byte pawnPosition, byte steps, Player currentPlayerTurn);
        bool CanMovePawn(Pawn pawn, byte steps);
        bool ReleasePawnFromBase(Player player);
    }
}
