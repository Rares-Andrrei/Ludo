using Ludo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.Model
{
    public class Board : IBoard
    {
        private const byte TilesNumber = 52;
        private const byte PlayerAreaTilesNumber = 5;

        private List<Player> _players;
        private Dictionary<Player, byte> PlayerHouseExitTile;
        private Dictionary<Player, byte> PlayerMaximumTile;

        public List<Player> Tiles { get; set; }

        public Board(List<Player> players)
        {
            InitializeBoard(players);
        }

        private void InitializeBoard(List<Player> players)
        {
            Tiles = Enumerable.Repeat<Player>(null, TilesNumber).ToList();

            List<Player.PlayerColor> playerColorsList = new List<Player.PlayerColor>(Enum.GetValues(typeof(Player.PlayerColor)) as Player.PlayerColor[]);
            _players = playerColorsList.Select(x => new Player { Color = x }).ToList();
        }


    }
}
