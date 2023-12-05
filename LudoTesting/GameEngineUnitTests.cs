using FluentAssertions;
using Ludo.Model;
using Ludo.Model.Interfaces;

namespace LudoTesting
{
    [TestClass]
    public class GameEngineUnitTests
    {
        private string[] _playerNames;
        private IGameEngine _gameEngine;

        [TestInitialize]
        public void TestInitialize()
        {
            _playerNames = new string[] { "Rares", "Elena", "Catalin" };
        }

        [TestMethod]
        public void Cosntructor_CorrectInitialization_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            _gameEngine.Players.Count.Should().Be(3);
        }

        [TestMethod]
        public void RollDice_ReturnValueBetween1And6_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            for (int i = 0; i < 10; i++)
            {
                byte value = _gameEngine.RollDice();

                value.Should().BeInRange(1, 6);
            }
        }

        [TestMethod]
        public void RollDice_ReturnRandomValues_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            byte distinct = 0;

            for (int i = 0; i < 8; i++)
            {
                byte value1 = _gameEngine.RollDice();
                byte value2 = _gameEngine.RollDice();
                if (value1 != value2)
                {
                    distinct++;
                }
            }
            distinct.Should().BeGreaterThan(3);
        }

        [TestMethod]
        public void CurrentPlayerTurn_ReturnTheCurrentPLayer_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            Player player = _gameEngine.CurrentPlayerTurn;

            player.Should().Be(_gameEngine.Players[0]);
        }

        [TestMethod]
        public void NextPlayerTurn_ReturnTheNextPlayer_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            _gameEngine.FinishedTurn();

            Player player = _gameEngine.CurrentPlayerTurn;

            player.Should().Be(_gameEngine.Players[1]);
        }

        [TestMethod]
        public void CurrentPlayerTurn_CheckIfTheCurrentPLayerWasUpdated_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            _gameEngine.FinishedTurn();
            Player player = _gameEngine.CurrentPlayerTurn;

            player.Should().Be(_gameEngine.Players[1]);
        }


        [TestMethod]
        public void NextPlayerTurn_CheckIfTheTurnLoopIsWorking_WhenCalled()
        {
            Player player;

            _gameEngine = new GameEngine(_playerNames);

            player = _gameEngine.CurrentPlayerTurn;
            player.Should().Be(_gameEngine.Players[0]);

            _gameEngine.FinishedTurn();
            player = _gameEngine.CurrentPlayerTurn;
            player.Should().Be(_gameEngine.Players[1]);

            _gameEngine.FinishedTurn();
            player = _gameEngine.CurrentPlayerTurn;
            player.Should().Be(_gameEngine.Players[2]);

            _gameEngine.FinishedTurn();
            player = _gameEngine.CurrentPlayerTurn;
            player.Should().Be(_gameEngine.Players[0]);
        }

        [TestMethod]
        public void CheckWinState_CheckIfTheWinStateIsCorrect_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            Player player = _gameEngine.CheckWinState();    

            player.Should().BeNull();
        }

        [TestMethod]
        public void ReleasePawnFromBase_CheckIfThePawnIsCorrectlyReleased_WhenCalled()
        {
            _gameEngine = new GameEngine(_playerNames);

            foreach(var pawn in _gameEngine.Players[0].Pawns)
            {
                pawn.State = Pawn.PawnState.Finished;
            }

            Player winner = _gameEngine.CheckWinState();

            winner.Should().Be(_gameEngine.Players[0]);
        }
    }


}