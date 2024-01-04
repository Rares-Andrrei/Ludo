using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo.Service;
using Ludo.ViewModels;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;
using Ludo_Backend.Observer;
using Moq;
namespace LudoTesting
{
    [TestClass]
    public class ObserverMockingTest
    {
        private Mock<IGameEngineOberver> _mockObserver;

        private IGameEngine _gameEngine;
        private BoardVM _boardVM;
        private Board _boardLogic;
        private string[] playerName;
        private List<Player> playerList;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockObserver = new Mock<IGameEngineOberver>();


            playerName = new string[] { "Rares", "Elena", "Catalin" };
            List<string>playerNames=new List<string>(playerName);
            playerList = new List<Player>();
            foreach (var name in playerName)
            {
                playerList.Add(new Player { Name = name });
            }

            _boardLogic = new Board(playerList);
            _gameEngine = new GameEngine(playerNames);
            _gameEngine.Attach(_mockObserver.Object);
            _boardLogic.Attach(_mockObserver.Object);


        }

        [TestMethod]
        public void NotifyReleasePawnFromBase_NotifyCorrectly_WhenCalled()
        {

            _boardLogic.ReleasePawnFromBase(playerList[0]);
            _mockObserver.Verify(observer => observer.NotifyPawnReleasedFromBase(It.IsAny<Player.PlayerColor>(), It.IsAny<byte>()), Times.Once());

        }
        [TestMethod]
        public void NotifyMoveInMade_NotifyCorrectly_WhenCalled()
        {
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 3, playerList[0]);
            _mockObserver.Verify(observer => observer.NotifyInPlayPawnMoveMade(It.IsAny<byte>(), It.IsAny<byte>()), Times.Once());

        }
        [TestMethod]
        public void NotifyInPlayToAlmostFinishedPawnMoveMade_NotifyObserverCorrectly_WhenCalled()
        {
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            for(int i=0;i<11;i++)
            {
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 5, playerList[0]);
            }
            _boardLogic.MoveAlmostFinishedPawn(playerList[0].Color, playerList[0].Pawns[0].Position, 5);
            _mockObserver.Verify(observer => observer.NotifyInPlayToAlmostFinishedPawnMoveMade(It.IsAny<Player.PlayerColor>(), It.IsAny<byte>(),It.IsAny<byte>()), Times.Once());
        }
        [TestMethod]
        public void NotifyDiceRolled_NotifyObserverCorrectly_WhenCalled()
        {
            _gameEngine.RollDice();
            //Console.WriteLine(_gameEngine.RollDice());
            _mockObserver.Verify(observer => observer.NotifyDiceRolled(It.IsAny<byte>()), Times.Once);
        }
        [TestMethod]
        public void NotifyScoreChanged_ShouldNotifyOnce_WhenMoveInPlayCalled()
        {
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            for (int i = 0; i < 10; i++)
            {
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 5, playerList[0]);
            }
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 6, playerList[0]);
            _mockObserver.Verify(observer => observer.NotifyScoreCanged(It.IsAny<Player.PlayerColor>(), It.IsAny<byte>()), Times.Once);
        }
        [TestMethod]
        public void NotifyPawnFinished_ShouldNotifyOnce()
        {
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            for(int i=0;i<10;i++)
            {
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 5, playerList[0]);
            }
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 6, playerList[0]);
            _mockObserver.Verify(observer => observer.NotifyPawnFinished(It.IsAny<Pawn.PawnState>(), It.IsAny<Player.PlayerColor>(), It.IsAny<byte>()), Times.Once);
        
        }
        [TestMethod]
        public void NotifyGameFinished_ShouldNotifyOnce()
        {
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            _boardLogic.ReleasePawnFromBase(playerList[0]);
            for (int i = 0; i < 10; i++)
            {
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 5, playerList[0]);
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[1].Position, 5, playerList[0]);
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[2].Position, 5, playerList[0]);
                _boardLogic.MoveInPlayPawn(playerList[0].Pawns[3].Position, 5, playerList[0]);
            }
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[0].Position, 6, playerList[0]);
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[1].Position, 6, playerList[0]);
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[2].Position, 6, playerList[0]);
            _boardLogic.MoveInPlayPawn(playerList[0].Pawns[3].Position, 6, playerList[0]);
            _mockObserver.Verify(observer => observer.NotifyGameFinished(It.IsAny<Player.PlayerColor>()), Times.Once);
        }
    }
}

