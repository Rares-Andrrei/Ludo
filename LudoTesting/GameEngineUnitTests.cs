using FluentAssertions;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;

namespace LudoTesting
{
    [TestClass]
    public class GameEngineUnitTests
    {
        private List<string> _playerNames;

        [TestInitialize]
        public void TestInitialize()
        {
            _playerNames = new List<string> { "Rares", "Elena","Catalin","Nicu" };
        }

        [TestMethod]
        public void Cosntructor_CorrectInitialization_WhenCalled()
        {
            var gameEngine = new GameEngine(_playerNames);

            gameEngine.Players.Count.Should().BeInRange(2, 4);
        }

        [TestMethod]
        public void RollDice_ReturnValueBetween1And6_WhenCalled()
        {
            var gameEngine = new GameEngine(_playerNames);

            for (int i = 0; i < 10; i++)
            {
                byte value = gameEngine.RollDice();

                value.Should().BeInRange(1, 6);
            }
        }

        [TestMethod]
        public void RollDice_ReturnRandomValues_WhenCalled()
        {
            var gameEngine = new GameEngine(_playerNames);

            byte distinct = 0;

            for (int i = 0; i < 8; i++)
            {
                byte value1 = gameEngine.RollDice();
                byte value2 = gameEngine.RollDice();
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
            var gameEngine = new GameEngine(_playerNames);

            Player player = gameEngine.CurrentPlayerTurn;

            player.Should().Be(gameEngine.Players[0]);
        }

        [TestMethod]
        public void NextPlayerTurn_ReturnTheNextPlayer_WhenCalled()
        {
            var gameEngine = new GameEngine(_playerNames);

            gameEngine.FinishedTurn();

            Player player = gameEngine.CurrentPlayerTurn;

            player.Should().Be(gameEngine.Players[1]);
        }

        [TestMethod]
        public void CurrentPlayerTurn_CheckIfTheCurrentPLayerWasUpdated_WhenCalled()
        {
            var gameEngine = new GameEngine(_playerNames);

            gameEngine.FinishedTurn();
            Player player = gameEngine.CurrentPlayerTurn;

            player.Should().Be(gameEngine.Players[1]);
        }


        [TestMethod]
        public void NextPlayerTurn_CheckIfTheTurnLoopIsWorking_WhenCalled()
        {
            Player player;

            var gameEngine = new GameEngine(_playerNames);
            
            for(int i=0;i<_playerNames.Count;i++)
            {
                player=gameEngine.CurrentPlayerTurn;
                player.Should().Be(gameEngine.Players[i]);
                gameEngine.FinishedTurn();
            }
        }
       
    }


}