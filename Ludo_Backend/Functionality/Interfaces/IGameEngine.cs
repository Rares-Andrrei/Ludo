using Ludo_Backend.Observer;
using System.Collections.Generic;

namespace Ludo_Backend.Functionaity.Interfaces
{
    public interface IGameEngine : IGameEngineSubject
    {
        List<Player> Players { get; set; }
        Player CurrentPlayerTurn { get; set; }

        void FinishedTurn();
        void MoveInPlayPawn(byte pawnPosition);
        bool MoveAlmostFinishedPawn(Player.PlayerColor playerColor, byte position);
        List<Pawn> AvailablePawnsToMoveForCurrentPlayer();
        bool ReleasePawnFromBaseCurrentPlayer();
        byte RollDice();
    }
}
