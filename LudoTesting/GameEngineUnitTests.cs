using Ludo;
using Ludo.Model;
using Newtonsoft.Json.Linq;

namespace LudoTesting
{
    [TestClass]
    public class GameEngineUnitTests
    {
        [TestMethod]
        public void TestRollDice()
        {
            GameEngine gm = new GameEngine();

            for (int i = 0; i < 10; i++)
            {
                byte value = gm.RollDice();
                Assert.AreEqual(value >= 1 && value <= 6, true);
            }

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
            Assert.AreEqual(distinct > 3, true);
        }
    }
}