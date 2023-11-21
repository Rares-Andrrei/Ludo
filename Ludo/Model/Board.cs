using Ludo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Ludo.Model
{
    public class Board : IBoard
    {
        private const byte TilesNumber = 52;
        private const byte PlayerAreaTilesNumber = 5;

        private List<Player> _players;
        private Dictionary<Player, byte> PlayerHouseExitTile;
        private Dictionary<Player, byte> PlayerMaximumTile;

        public List<Tile> Tiles { get; set; }

        public Board(List<Player> players)
        {
            InitializeBoard(players);
        }

        private void InitializeBoard(List<Player> players)
        {
            for (byte i = 0; i < TilesNumber; i++)
            {
                Tiles[i] = new Tile { BoardPosition = i, IsSafezone = false, CurrentOwner = null };
            }
            const byte maxPLayers = 4;
            List<byte> startingPositions = new List<byte> { 0, 13, 26, 39 };
            List<byte> endPositions = new List<byte> {50, 11, 24, 37 };
            List<Player.PlayerColor> playerColors = Enum.GetValues(typeof(Player.PlayerColor)).Cast<Player.PlayerColor>().ToList();

            for (byte i = 0; i < maxPLayers; i++)
            {
                Tiles[startingPositions[i]].IsSafezone = true;
                if (players.Count > i)
                {
                    players[i].Color = playerColors[i];
                    players[i].StartPosition = startingPositions[i];
                    players[i].EndPosition = endPositions[i];
                }
            }
        }


    }
}
