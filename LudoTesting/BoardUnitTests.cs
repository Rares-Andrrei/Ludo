﻿using FluentAssertions;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;

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
    }
}
