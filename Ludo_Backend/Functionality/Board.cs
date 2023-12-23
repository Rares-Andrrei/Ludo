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
        public List<Pawn> InAlmostFinishedPathPawns { get; set; }

        public Board(List<Player> players)
        {
            InAlmostFinishedPathPawns = new List<Pawn>();
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

        private byte CalculateDestination(byte pawnPosition, byte steps)
        {
            byte destination = (byte)(pawnPosition + steps);

            if (destination >= TilesNumber)
            {
                destination -= TilesNumber;
            }

            return destination;
        }

        private void PlayerScored(Player player)
        {
            player.Score++;
            observers.ForEach(observer => observer.NotifyScoreCanged(player.Color, player.Score));
            if (player.Score == 4)
            {
                observers.ForEach(observer => observer.NotifyGameFinished(player.Color));
            }
        }

        public bool MoveInPlayPawn(byte pawnPosition, byte steps, Player currentPlayer)
        {
            byte destination = CalculateDestination(pawnPosition, steps);

            Pawn pawn = Tiles[pawnPosition].CurrentOwnerPawns.Where(oPawn => oPawn.Owner == currentPlayer).FirstOrDefault();

            if(pawn == null)
            {
                return false;
            }

            if (pawn.State == Pawn.PawnState.InPlay)
            {
                Tiles[pawnPosition].CurrentOwnerPawns.Remove(pawn);

                if (pawn.Position == pawn.Owner.EndPosition && steps == 6)
                {
                    pawn.State = Pawn.PawnState.Finished;
                    PlayerScored(pawn.Owner);
                    observers.ForEach(observer => observer.NotifyPawnFinished(Pawn.PawnState.InPlay, pawn.Owner.Color, pawn.Position));

                }
                else if (pawnPosition + steps > pawn.Owner.EndPosition && pawnPosition <= pawn.Owner.EndPosition)
                {
                    int extraSteps = pawnPosition + steps - pawn.Owner.EndPosition - 1;
                    pawn.State = Pawn.PawnState.AlmostFinished;
                    pawn.Position = (byte)extraSteps;
                    InAlmostFinishedPathPawns.Add(pawn);
                    observers.ForEach(observer => observer.NotifyInPlayToAlmostFinishedPawnMoveMade(pawn.Owner.Color, pawnPosition, (byte)extraSteps));
                }
                else
                {
                    if (CheckCollision(pawn, destination))
                    {
                        Tiles[pawnPosition].CurrentOwnerPawns.Add(pawn);
                        observers.ForEach(observer => observer.NotifyCollision());
                        return true;
                    }

                    AddPawnToTile(pawn, destination);
                    observers.ForEach(observer => observer.NotifyInPlayPawnMoveMade(pawnPosition, destination));
                }
                return true;
            }
            return false;
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

        private void AddPawnToTile(Pawn pawn, byte tileNr)
        {
            Tiles[tileNr].CurrentOwnerPawns.Add(pawn);
            pawn.Position = tileNr;
        }

        public bool IsMoveValid(byte pawnPosition, byte steps, Player currentPlayerTurn)
        {

            byte destination = (byte)(pawnPosition + steps);
            Pawn pawn = Tiles[pawnPosition].CurrentOwnerPawns.Where(oPawn => oPawn.Owner == currentPlayerTurn).FirstOrDefault();
            if (pawn == null)
            {
                return false;
            }
            else if(pawn.Owner != currentPlayerTurn)
            {
                return false;
            }
            else if (pawn.State == Pawn.PawnState.AlmostFinished && destination > PlayerAreaTilesNumber + 1)
            {
                return false;
            }

            return true;
        }

        public bool CanMovePawn(Pawn pawn, byte steps)
        {
            byte destination = (byte)(pawn.Position + steps);

            if (pawn.State == Pawn.PawnState.Finished)
            {
                return false;
            }
            else if(pawn.State == Pawn.PawnState.InBase && steps != 6)
            {
                return false;
            }
            else if (pawn.State == Pawn.PawnState.AlmostFinished && destination > PlayerAreaTilesNumber)
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

        public bool MoveAlmostFinishedPawn(Player.PlayerColor playerColor, byte position, byte steps)
        {
            byte destination = (byte)(position + steps);

            Pawn pawn = InAlmostFinishedPathPawns.Where(oPawn => oPawn.Owner.Color == playerColor && oPawn.Position == position).FirstOrDefault();

            if (pawn == null)
            {
                return false;
            }

            if (pawn.State == Pawn.PawnState.AlmostFinished)
            {
                if (destination == PlayerAreaTilesNumber)
                {
                    pawn.State = Pawn.PawnState.Finished;
                    PlayerScored(pawn.Owner);
                    observers.ForEach(observer => observer.NotifyPawnFinished(Pawn.PawnState.AlmostFinished, pawn.Owner.Color, pawn.Position));
                }
                else if (destination > PlayerAreaTilesNumber)
                {
                    return false;
                }
                else
                {
                    pawn.Position += steps;
                    observers.ForEach(observer => observer.NotifyAlmostFinishedPawnMoveMade(pawn.Owner.Color, position, destination));
                }
            }
            return true;
        }
    }
}
