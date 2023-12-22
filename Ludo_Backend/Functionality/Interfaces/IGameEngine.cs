using Ludo_Backend.Observer;
using System.Collections.Generic;

namespace Ludo_Backend.Functionaity.Interfaces
{
    public interface IGameEngine : IGameEngineSubject
    {
        List<Player> Players { get; set; }
        Player CurrentPlayerTurn { get; set; }

        void FinishedTurn();
        void MovePawn(byte pawnPosition);
        bool CanMovePawn(Pawn pawn, byte steps);
        List<Pawn> AvailablePawnsToMoveForCurrentPlayer();
        Player CheckWinState();
        bool ReleasePawnFromBaseCurrentPlayer();
        byte RollDice();
    }
}
