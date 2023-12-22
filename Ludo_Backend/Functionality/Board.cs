using Ludo_Backend.Functionaity.Interfaces;
using Ludo_Backend.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo_Backend.Functionaity
{
    public class Board : IBoard
    {
        private List<IGameEngineOberver> observers;
        private byte MaximumPlayesNr = 4;
        private const byte TilesNumber = 52;
        private const byte PlayerAreaTilesNumber = 5;

        public List<Tile> Tiles { get; set; }

        public Board(List<Player> players)
        {
            observers = new List<IGameEngineOberver>();
            InitializeBoard(players);
            InitializePawns(players);
        }

        private void InitializePawns(List<Player> players)
        {
            byte nrOfPawns = 4;

            foreach (var player in players)
            {                
                for(byte i = 0; i < nrOfPawns; i++)
                {
                    Pawn pawn = new Pawn(player);
                    player.Pawns.Add(pawn);
                }
            }
        }

        private void InitializeBoard(List<Player> players)
        {
            Tiles = Enumerable.Repeat<Tile>(null, 52).ToList();

            for (byte i = 0; i < TilesNumber; i++)
            {
                Tiles[i] = new Tile { BoardPosition = i, IsSafezone = false, CurrentOwnerPawns = new List<Pawn>() };
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

        public bool MoveInPlayPawn(byte pawnPosition, byte steps)
        {
            Pawn pawn = Tiles[pawnPosition].CurrentOwnerPawns.FirstOrDefault();

            if(pawn == null)
            {
                return false;
            }

            byte destination = (byte)(pawn.Position + steps);

            if (pawn.State == Pawn.PawnState.InBase || pawn.State == Pawn.PawnState.Finished)
            {
                return false;
            }

            if (pawn.State == Pawn.PawnState.InPlay)
            {
                RemovePawnFromOldTile(pawn, pawn.Position);

                if (pawn.Position == pawn.Owner.EndPosition && steps == 6)
                {
                    pawn.State = Pawn.PawnState.Finished;
                }
                else if (destination > pawn.Owner.EndPosition)
                {
                    int extraSteps = destination - pawn.Owner.EndPosition;
                    pawn.State = Pawn.PawnState.AlmostFinished;
                    pawn.Position = (byte)extraSteps;
                }
                else
                {
                    if (CheckCollision(pawn, destination))
                    {
                        SolveCollision(destination);
                    }

                    AddPawnToTile(pawn, destination);
                    observers.ForEach(observer => observer.NotifyInPlayPawnMoveMade(pawnPosition, steps));
                }
            }
            else if (pawn.State == Pawn.PawnState.AlmostFinished)
            {
                if (destination == PlayerAreaTilesNumber + 1)
                {
                    pawn.State = Pawn.PawnState.Finished;
                }
                else if (destination > PlayerAreaTilesNumber + 1)
                {
                    return false;
                }
                else
                {
                    pawn.Position += steps;
                }
            }
            return true;
        }

        private bool CheckCollision(Pawn pawn, byte destination)
        {
            if (Tiles[destination].IsSafezone)
            {
                return false;
            }
            if (Tiles[destination].CurrentOwnerPawns.Count != 0 && Tiles[destination].CurrentOwnerPawns.First().Owner != pawn.Owner)
            {
                return true;
            }
            return false;
        }

        private void SolveCollision(byte destination)
        {
            foreach (Pawn enemyPawn in Tiles[destination].CurrentOwnerPawns)
            {
                enemyPawn.Position = enemyPawn.Owner.StartPosition;
                enemyPawn.State = Pawn.PawnState.InBase;
                Tiles[destination].CurrentOwnerPawns.Remove(enemyPawn);
            }
        }

        private void AddPawnToTile(Pawn pawn, byte tileNr)
        {
            Tiles[tileNr].CurrentOwnerPawns.Add(pawn);
            pawn.Position = tileNr;
        }

        private void RemovePawnFromOldTile(Pawn pawn, byte tileNr)
        {
            Tiles[tileNr].CurrentOwnerPawns.Remove(pawn);
        }

        public bool CanMovePawn(Pawn pawn, byte steps)
        {
            byte destination = (byte)(pawn.Position + steps);

            if (pawn.State == Pawn.PawnState.InBase && steps != 6)
            {
                return false;
            }
            else if (pawn.State == Pawn.PawnState.AlmostFinished && destination > PlayerAreaTilesNumber + 1)
            {
                return false;
            }

            return true;
        }

        public bool ReleasePawnFromBase(Player player)
        {
            Pawn playerPawn = player.Pawns.FirstOrDefault(pawn => pawn.State == Pawn.PawnState.InBase);

            if (playerPawn == null)
            {
                return false;
            }

            playerPawn.State = Pawn.PawnState.InPlay;
            AddPawnToTile(playerPawn, playerPawn.Owner.StartPosition);
            observers.ForEach(observer => observer.NotifyPawnReleasedFromBase(playerPawn.Owner.Color, playerPawn.Owner.StartPosition));
            return true;
        }

        public void Attach(IGameEngineOberver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IGameEngineOberver observer)
        {
            observers.Remove(observer);
        }
    }
}
