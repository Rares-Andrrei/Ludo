using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.ViewModels
{
    public class BoardVM
    {
        private const byte TilesNr = 52;
        private const byte PawnsNr = 4;

        private IGameEngine _gameEngine;
        public List<string> Tiles { get; set; }
        public Player CurrentPlayerTurn { get; set; }
        public string DiceImage { get; set; }


        public List<string> RedBase { get; set; }
        public byte RedPlayerScore { get; set; }
        public string RedPlayerName { get; set; }

        public List<string> BlueBase { get; set; }
        public byte BluePlayerScore { get; set; }
        public string BluePlayerName { get; set; }

        public List<string> GreenBase { get; set; }
        public byte GreenPlayerScore { get; set; }
        public string GreenPlayerName { get; set; }

        public List<string> YellowBase { get; set; }
        public byte YellowPlayerScore { get; set; }
        public string YellowPlayerName { get; set; }

        private void InitializeViewModel()
        {
            Tiles = Enumerable.Repeat<string>(null, TilesNr).ToList();
            RedBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\redPawn.png", PawnsNr).ToList();
            BlueBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\bluePawn.png", PawnsNr).ToList();
            GreenBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\greenPawn.png", PawnsNr).ToList();
            YellowBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\yellowPawn.png", PawnsNr).ToList();

            RedPlayerScore = 0;
            BluePlayerScore = 0;
            GreenPlayerScore = 0;
            YellowPlayerScore = 0;
        }

        public BoardVM()
        {
            InitializeViewModel();
        }

    }
}
