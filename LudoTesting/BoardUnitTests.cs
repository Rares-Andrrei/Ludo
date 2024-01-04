using FluentAssertions;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;

namespace LudoTesting
{
    [TestClass]
    public class BoardUnitTests
    {
        private string[] _playerNames;
        private string[] _playerColors;
        private List<Player> _players;
        private Board _board;

        [TestInitialize]
        public void TestInitialize()
        {
            _playerNames = new string[] { "Rares", "Elena", "Catalin" };
            _playerColors = new string[] { "Red", "Blue", "Green" };
            _players = new List<Player>();
            foreach (string name in _playerNames)
            {
                int i = 0;

                _players.Add(new Player { Name = name, Color = Player.StringToPlayerColor(_playerColors[i]) });
                i++;
            }
        }
        [TestMethod]
        public void MoveInPlayPawn_DoingStepCorrectlyFromBase_WhenCalled()
        {

            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            board.ReleasePawnFromBase(_players[1]);

            bool moveResult = board.MoveInPlayPawn(0, 2, _players[0]);
            bool moveResult2 = board.MoveInPlayPawn(13, 5, _players[1]);
            Assert.IsTrue(moveResult);
            //Assert.IsTrue(moveResult2);
            Assert.AreEqual(2, _players[0].Pawns[0].Position);
            Assert.AreEqual(18, _players[1].Pawns[0].Position);

        }
        [TestMethod]
        public void ReleasePawn_CorrectlyPosition_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            board.ReleasePawnFromBase(_players[1]);
            board.ReleasePawnFromBase(_players[2]);


            Assert.AreEqual(0, _players[0].Pawns[0].Position);
            Assert.AreEqual(13, _players[1].Pawns[0].Position);
            Assert.AreEqual(26, _players[2].Pawns[0].Position);

        }

        [TestMethod]
        public void IsMoveValid_CheckIfMoveReallyValid_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);

            for (int i = 0; i < 5; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;
               
               board.MoveInPlayPawn(newPosition, 5, _players[0]);

            }
            bool validMove = board.IsMoveValid(_players[0].Pawns[0].Position, 1, _players[0]);
           
            Assert.IsTrue(validMove,"Out of table");


        }
        [TestMethod]
        public void IsMoveValid_CheckIfPawnHaveCorrectOwner_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);

            for (int i = 0; i < 5; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;

                board.MoveInPlayPawn(newPosition, 5, _players[0]);

            }
            bool validMove = board.IsMoveValid(_players[0].Pawns[0].Position, 1, _players[0]);
            Assert.IsTrue(validMove,"Pawn have ONE owner");
        }
        [TestMethod]
        public void IsMoveValid_CheckIfPawnIsInPlay_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            board.ReleasePawnFromBase(_players[0]);

            for (int i = 0; i < 11; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;

                board.MoveInPlayPawn(newPosition, 5, _players[0]);

            }
            bool validMove2 = board.IsMoveValid(_players[0].Pawns[1].Position, 1, _players[0]);
             Assert.IsTrue(validMove2,"Pawn cannot be null");
        }
        [TestMethod]
        public void CanMove_CheckIfThePawnCanMoveWithoutCollision_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            board.ReleasePawnFromBase(_players[1]);
            for (int i = 0; i < 9; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;
                board.MoveInPlayPawn(newPosition, 5, _players[0]);
               

            }
           
            bool canMove = board.CanMovePawn(_players[1].Pawns[0], 2);
            Assert.IsTrue(canMove,"Collision detected");

            
        }
        [TestMethod]
        public void CanMove_CheckIfThePawnCanMoveWithoutOutOfBoardMove_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            board.ReleasePawnFromBase(_players[1]);
            for (int i = 0; i < 10; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;
                board.MoveInPlayPawn(newPosition, 5, _players[0]);


            }
          
            bool canMove2 = board.CanMovePawn(_players[0].Pawns[0], 2);
            Console.WriteLine(_players[0].Pawns[0].Position);
            Assert.IsTrue(canMove2, "Out of table");

        }
        [TestMethod]
        public void MoveAlmostFinishedPawn_SuccesfullyMoveInFinishedState_WhenCalled()
        {
            var board = new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            var playerColor = _players[0].Color;
            for (int i = 0; i < 10; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;
                board.MoveInPlayPawn(newPosition, 5, _players[0]);
            }
            board.MoveInPlayPawn(_players[0].Pawns[0].Position,1, _players[0]);
            bool moveResult = board.MoveAlmostFinishedPawn(playerColor, _players[0].Pawns[0].Position, 5);
            Console.WriteLine(_players[0].Pawns[0].Position);
             Assert.IsTrue(moveResult, "Expected successful move");
            
            Assert.AreEqual(Pawn.PawnState.Finished, _players[0].Pawns[0].State, "Expected pawn to be in Finished state");


        }
        [TestMethod]
        public void MoveAlmostFinishedPawn_SuccesfullyMoveInAlmostFinishedState_WhenCalled()
        {
            var board=new Board(_players);
            board.ReleasePawnFromBase(_players[0]);
            var playerColor = _players[0].Color;
            for (int i = 0; i < 10; i++)
            {
                byte newPosition = _players[0].Pawns[0].Position;
                board.MoveInPlayPawn(newPosition, 5, _players[0]);
            }
           bool move= board.MoveInPlayPawn(_players[0].Pawns[0].Position, 1, _players[0]);
            Assert.IsTrue(move, "Expected successful move");

            Assert.AreEqual(Pawn.PawnState.AlmostFinished, _players[0].Pawns[0].State, "Expected pawn to be in Almost Finished state");
        }

    }
}
