using Ludo_Backend.Functionaity.Interfaces;
using Ludo_Backend.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo_Backend.Functionaity
{
    public class GameEngine : IGameEngine
    {
        private const byte MaximumPlayesNr = 4;

        private byte _diceValue;
        private List<IGameEngineOberver> observers;
        private readonly Random random = new Random();
        private IBoard board;

        public List<Player> Players { get; set; }
        public Player CurrentPlayerTurn { get; set; }

        public GameEngine(List<string> playersNames)
        {
            observers = new List<IGameEngineOberver>();
            InitializeGame(playersNames);
        }

        private void InitializeGame(List<string> playersNames)
        {
            byte playersNr = (byte)playersNames.Count;

            if (playersNr > MaximumPlayesNr || playersNr < 2)
            {
                throw new Exception("Invalid number of players");
            }

            Players = playersNames.Select(name => new Player { Name = name }).ToList();
            board = new Board(Players);
            CurrentPlayerTurn = Players[0];
        }

        public void FinishedTurn()
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
        }

        public byte RollDice()
        {       
            _diceValue = (byte)random.Next(1, 7);
            observers.ForEach(observer => observer.NotifyDiceRolled(_diceValue));
            return _diceValue;
        }

        public void MovePawn(byte pawnPosition)
        {
            board.MoveInPlayPawn(pawnPosition, _diceValue);
        }

        public bool CanMovePawn(Pawn pawn, byte steps)
        {
            return board.CanMovePawn(pawn, steps);
        }

        public bool ReleasePawnFromBaseCurrentPlayer()
        {
            if(_diceValue != 6)
            {
                return false;
            }
            return board.ReleasePawnFromBase(CurrentPlayerTurn);
        }

        public Player CheckWinState()
        {
            foreach (Player player in Players)
            {
                if (player.Pawns.All(pawn => pawn.State == Pawn.PawnState.Finished))
                {
                    return player;
                }
            }

            return null;
        }

        public void Attach(IGameEngineOberver observer)
        {
            observers.Add(observer);
            board.Attach(observer);
        }

        public void Detach(IGameEngineOberver observer)
        {
            observers.Remove(observer);
            board.Detach(observer);
        }

        public List<Pawn> AvailablePawnsToMoveForCurrentPlayer()
        {
            List<Pawn> pawns = new List<Pawn>();
            CurrentPlayerTurn.Pawns.ForEach(pawn =>
            {
                if (CanMovePawn(pawn, _diceValue))
                {
                    pawns.Add(pawn);
                }
            });

            return pawns;
        }
    }
}
