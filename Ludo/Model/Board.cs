using Ludo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.Model
{
    public class Board : IBoard
    {
        private byte MaximumPlayesNr = 4;
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
            Tiles = Enumerable.Repeat<Tile>(null, 52).ToList();

            for (byte i = 0; i < TilesNumber; i++)
            {
                Tiles[i] = new Tile { BoardPosition = i, IsSafezone = false, CurrentOwner = null };
            }

            List<byte> startingPositions = new List<byte> { 0, 13, 26, 39 };
            List<byte> endPositions = new List<byte> { 50, 11, 24, 37 };
            List<Player.PlayerColor> playerColors = Enum.GetValues(typeof(Player.PlayerColor)).Cast<Player.PlayerColor>().ToList();

            for (byte i = 0; i < MaximumPlayesNr; i++)
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
