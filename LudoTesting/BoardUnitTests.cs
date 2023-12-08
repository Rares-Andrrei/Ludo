using FluentAssertions;
using Ludo_Backend.Model;
using Ludo_Backend.Model.Interfaces;

namespace LudoTesting
{
    [TestClass]
    public class BoardUnitTests
    {
        private string[] _playerNames;
        private List<Player> _players;
        private IBoard _board;

        [TestInitialize]
        public void TestInitialize()
        {
            _playerNames = new string[] { "Rares", "Elena", "Catalin" };
            _players = new List<Player>();
            foreach (string name in _playerNames)
            {
                _players.Add(new Player { Name = name });
            }
        }

        [TestMethod]
        public void Cosntructor_CorrectInitialization_WhenCalled()
        {
            _board = new Board(_players);

            _board.Tiles.Count.Should().Be(52);
        }

    }
}
