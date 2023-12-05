using System.Collections.Generic;

namespace Ludo.Model.Interfaces
{
    public interface IGameEngine
    {
        List<Player> Players { get; set; }
        Player CurrentPlayerTurn { get; set; }

        void FinishedTurn();
        bool MovePawn(Pawn pawn, byte steps);
        bool CanMovePawn(Pawn pawn, byte steps);
        Player CheckWinState();
        bool ReleasePawnFromBase(Pawn pawn);
        byte RollDice();
    }
}
