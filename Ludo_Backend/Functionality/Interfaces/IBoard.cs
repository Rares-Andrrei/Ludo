using Ludo_Backend.Observer;
using System.Collections.Generic;

namespace Ludo_Backend.Functionaity.Interfaces
{
    public interface IBoard : IGameEngineSubject
    {
        List<Tile> Tiles { get; set; }

        bool MoveInPlayPawn(byte pawnPosition, byte steps);
        bool CanMovePawn(Pawn pawn, byte steps);
        bool ReleasePawnFromBase(Player player);
    }
}
