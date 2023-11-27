using System.Collections.Generic;

namespace Ludo.Model.Interfaces
{
    public interface IBoard
    {
        List<Tile> Tiles { get; set; }
    }
}
