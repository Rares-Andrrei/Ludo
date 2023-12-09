using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static Ludo_Backend.Functionaity.Player;

namespace Ludo.ViewModels
{
    public class BoardVM : BaseVM
    {

        public BoardVM()
        {
            InitializeViewModel();
        }

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

        public void InitializeGame(List<string> players)
        {
            _gameEngine = new GameEngine(players);
            UpdateWithGameData();
        }

        private void UpdateWithGameData()
        {
            _gameEngine.Players.ForEach(player =>
            {
                switch (player.Color)
                {
                    case PlayerColor.Red:
                        RedPlayerName = player.Name;
                        break;
                    case PlayerColor.Blue:
                        BluePlayerName = player.Name;
                        break;
                    case PlayerColor.Green:
                        GreenPlayerName = player.Name;
                        break;
                    case PlayerColor.Yellow:
                        YellowPlayerName = player.Name;
                        break;
                }
            });
            CurrentPlayerTurn = _gameEngine.CurrentPlayerTurn;
        }

        private const byte TilesNr = 52;
        private const byte PawnsNr = 4;

        private IGameEngine _gameEngine;
        public List<string> Tiles { get; set; }
        private Player _currentPlayerTurn;
        public Player CurrentPlayerTurn
        {
            get { return _currentPlayerTurn; }
            set
            {
                _currentPlayerTurn = value;
                OnPropertyChanged();
            }
        }

        public string DiceImage { get; set; }


        public List<string> RedBase { get; set; }
        public byte RedPlayerScore { get; set; }
        private string _redPlayerName;
        public string RedPlayerName
        {
            get { return _redPlayerName; }
            set
            {
                _redPlayerName = value;
                OnPropertyChanged();
            }
        }

        public List<string> BlueBase { get; set; }
        public byte BluePlayerScore { get; set; }
        private string _bluePlayerName;
        public string BluePlayerName
        {
            get { return _bluePlayerName; }
            set
            {
                _bluePlayerName = value;
                OnPropertyChanged();
            }
        }

        public List<string> GreenBase { get; set; }
        public byte GreenPlayerScore { get; set; }
        private string _greenPlayerName;
        public string GreenPlayerName
        {
            get { return _greenPlayerName; }
            set
            {
                _greenPlayerName = value;
                OnPropertyChanged();
            }
        }

        public List<string> YellowBase { get; set; }
        public byte YellowPlayerScore { get; set; }
        private string _yellowPlayerName;
        public string YellowPlayerName
        {
            get { return _yellowPlayerName; }
            set
            {
                _yellowPlayerName = value;
                OnPropertyChanged();
            }
        }

    }
}
