using Ludo.Model.Interfaces;
using System;

namespace Ludo.Model
{
    public class GameEngine : IGameEngine
    {
        private readonly Random random = new Random();
        public byte RollDice()
        {
            return (byte)random.Next(1, 7);
        }
    }
}
