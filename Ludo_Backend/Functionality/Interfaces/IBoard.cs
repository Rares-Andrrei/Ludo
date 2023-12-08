using System.Collections.Generic;

namespace Ludo_Backend.Functionaity.Interfaces
{
    public interface IBoard
    {
        List<Tile> Tiles { get; set; }

        bool MovePawn(Pawn pawn, byte steps);
        bool CanMovePawn(Pawn pawn, byte steps);
        bool ReleasePawnFromBase(Pawn pawn);
    }
}
