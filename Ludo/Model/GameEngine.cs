using Ludo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.Model
{
    public class GameEngine : IGameEngine
    {
        private const byte MaximumPlayesNr = 4;

        private readonly Random _random = new Random();
        private Board _board;

        public List<Player> Players { get; set; }
        public Player CurrentPlayerTurn { get; set; }

        public GameEngine(string[] playersNames)
        {
            InitializeGame(playersNames);
        }

        private void InitializeGame(string[] playersNames)
        {
            byte playersNr = (byte)playersNames.Length;

            if (playersNr > MaximumPlayesNr || playersNr < 2)
            {
                throw new Exception("Invalid number of players");
            }

            Players = playersNames.Select(name => new Player { Name = name }).ToList();
            _board = new Board(Players);
            CurrentPlayerTurn = Players[0];
        }

        public Player NextPlayerTurn()
        {
            byte index = (byte)Players.IndexOf(CurrentPlayerTurn);
            index += 1;

            if (index == Players.Count)
            {
                CurrentPlayerTurn = Players[0];
            }
            else
            {
                CurrentPlayerTurn = Players[index];
            }
            return CurrentPlayerTurn;
        }

        public byte RollDice()
        {
            return (byte)_random.Next(1, 7);
        }
    }
}
