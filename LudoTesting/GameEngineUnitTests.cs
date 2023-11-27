using FluentAssertions;
using Ludo.Model;

namespace LudoTesting
{
    [TestClass]
    public class GameEngineUnitTests
    {

        [TestMethod]
        public void RollDice_ReturnValueBetween1And6_WhenCalled()
        {
            GameEngine gm = new GameEngine();

            for (int i = 0; i < 10; i++)
            {
                byte value = gm.RollDice();

                value.Should().BeInRange(1, 6);
            }
        }

        [TestMethod]
        public void RollDice_ReturnRandomValues_WhenCalled()
        {
            GameEngine gm = new GameEngine();

            byte distinct = 0;

            for (int i = 0; i < 8; i++)
            {
                byte value1 = gm.RollDice();
                byte value2 = gm.RollDice();
                if (value1 != value2)
                {
                    distinct++;
                }
            }
            distinct.Should().BeGreaterThan(3);
        }
    }


}