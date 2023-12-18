using Ludo.Service;
using Ludo.View;
using Ludo.ViewModels.Commands;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;
using Ludo_Backend.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static Ludo_Backend.Functionaity.Player;

namespace Ludo.ViewModels
{
    public class BoardVM : BaseVM, IGameEngineOberver
    {
        private const byte TilesNr = 52;
        private const byte PawnsNr = 4;
        private const string defaultDice = "\\Images\\DiceImages\\dice.jpeg";

        private IGameEngine _gameEngine;
        public ObservableCollection<string> Tiles { get; set; }
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

        private bool _isDiceEanbled;
        private string _diceImage;
        public string DiceImage
        {
            get { return _diceImage; }
            set
            {
                _diceImage = value;
                OnPropertyChanged();
            }
        }


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

        public BoardVM()
        {
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Tiles = new ObservableCollection<string>(Enumerable.Repeat<string>(null, TilesNr).ToList());
            RedBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\redPawn.png", PawnsNr).ToList();
            BlueBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\bluePawn.png", PawnsNr).ToList();
            GreenBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\greenPawn.png", PawnsNr).ToList();
            YellowBase = Enumerable.Repeat<string>("\\Images\\PawnImages\\yellowPawn.png", PawnsNr).ToList();

            RedPlayerScore = 0;
            BluePlayerScore = 0;
            GreenPlayerScore = 0;
            YellowPlayerScore = 0;

            DiceImage = defaultDice;
        }
        public void InitializeGame(List<string> players)
        {
            _gameEngine = new GameEngine(players);
            _gameEngine.Attach(this);
            UpdatePlayerColors();
            NextPlayerTurn();
        }
        private void UpdatePlayerColors()
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
        }
        private void NextPlayerTurn()
        {
            CurrentPlayerTurn = _gameEngine.CurrentPlayerTurn;
            _isDiceEanbled = true;
        }

        private void ChangeDiceValue(byte diceValue)
        {
            switch(diceValue)
            {
                case 1:
                    DiceImage = "\\Images\\DiceImages\\one.png";
                    break;
                case 2:
                    DiceImage = "\\Images\\DiceImages\\two.png";
                    break;
                case 3:
                    DiceImage = "\\Images\\DiceImages\\three.png";
                    break;
                case 4:
                    DiceImage = "\\Images\\DiceImages\\four.png";
                    break;
                case 5:
                    DiceImage = "\\Images\\DiceImages\\five.png";
                    break;
                case 6:
                    DiceImage = "\\Images\\DiceImages\\six.png";
                    break;
                default:
                    DiceImage = defaultDice;
                    break;
            }
        }
        private void ShowAvailableMoves()
        {
            List<Pawn> availablePawns = _gameEngine.AvailablePawnsToMoveForCurrentPlayer();
            foreach (Pawn pawn in availablePawns)
            {
                string imageName = $"{CurrentPlayerTurn.Color.ToString().ToLower()}HighlitedPawn.png";
                Tiles[pawn.Position] = $"\\Images\\PawnImages\\imageName";
            }
        }

        public void NotifyDiceRolled(byte diceValue)
        {
            _isDiceEanbled = false;
            ChangeDiceValue(diceValue);
            ShowAvailableMoves();
        }

        #region Commands

        private ICommand _rollDiceCommand;
        public ICommand RollDiceCommand
        {
            get
            {
                if (_rollDiceCommand == null)
                    _rollDiceCommand = new RelayCommand(RollDice);
                return _rollDiceCommand;
            }
        }

        private void RollDice(object parameter)
        {
            if (_isDiceEanbled)
            {
                _gameEngine.RollDice();
            }
        }

        private ICommand _pawnSelected;
        public ICommand PawnSelectedCommand
        {
            get
            {
                if (_pawnSelected == null)
                    _pawnSelected = new RelayCommand(PawnSelected);
                return _pawnSelected;
            }
        }

        private void PawnSelected(object parameter)
        {

        }
        #endregion

    }
}
