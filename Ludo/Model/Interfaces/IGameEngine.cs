using System.Collections.Generic;

namespace Ludo.Model.Interfaces
{
    public interface IGameEngine
    {
        List<Player> Players { get; set; }
        Player CurrentPlayerTurn { get; set; }

        Player NextPlayerTurn();

        byte RollDice();
    }
}
