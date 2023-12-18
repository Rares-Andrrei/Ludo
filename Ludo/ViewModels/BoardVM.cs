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

        private ObservableCollection<string> _redBase;
        public ObservableCollection<string> RedBase
        {
            get { return _redBase; }
            set
            {
                _redBase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> RedPath { get; set; }
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

        private ObservableCollection<string> _blueBase;
        public ObservableCollection<string> BlueBase
        {
            get { return _blueBase; }
            set
            {
                _blueBase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> BluePath { get; set; }
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

        private ObservableCollection<string> _greenBase;
        public ObservableCollection<string> GreenBase
        {
            get { return _greenBase; }
            set
            {
                _greenBase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> GreenPath { get; set; }
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

        private ObservableCollection<string> _yellowBase;
        public ObservableCollection<string> YellowBase
        {
            get { return _yellowBase; }
            set
            {
                _yellowBase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> YellowPath { get; set; }
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

        private void InitializeViewModel()
        {
            Tiles = new ObservableCollection<string>(Enumerable.Repeat<string>(null, TilesNr).ToList());
            if (RedPlayerName != null)
            {
                RedBase = new ObservableCollection<string>(Enumerable.Repeat<string>("\\Images\\PawnImages\\redPawn.png", PawnsNr).ToList());
                RedPlayerScore = 0;
            }
            if (BluePlayerName != null)
            {
                BlueBase = new ObservableCollection<string>(Enumerable.Repeat<string>("\\Images\\PawnImages\\bluePawn.png", PawnsNr).ToList());
                BluePlayerScore = 0;
            }
            if (GreenPlayerName != null)
            {
                GreenBase = new ObservableCollection<string>(Enumerable.Repeat<string>("\\Images\\PawnImages\\greenPawn.png", PawnsNr).ToList());
                GreenPlayerScore = 0;
            }
            if (YellowPlayerName != null)
            {
                YellowBase = new ObservableCollection<string>(Enumerable.Repeat<string>("\\Images\\PawnImages\\yellowPawn.png", PawnsNr).ToList());
                YellowPlayerScore = 0;
            }

            DiceImage = defaultDice;
        }
        public void InitializeGame(List<string> players)
        {
            _gameEngine = new GameEngine(players);
            _gameEngine.Attach(this);
            UpdatePlayerColors();
            InitializeViewModel();
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
            switch (diceValue)
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
        private void ShowAvailableMoves(byte diceValue)
        {
            List<Pawn> availablePawns = _gameEngine.AvailablePawnsToMoveForCurrentPlayer();

            if (availablePawns.Count == 0)
            {
                _gameEngine.FinishedTurn();
                NextPlayerTurn();
                return;
            }

            foreach (Pawn pawn in availablePawns)
            {
                if (pawn.State == Pawn.PawnState.InPlay)
                {
                    string imageName = $"{CurrentPlayerTurn.Color.ToString().ToLower()}HighlitedPawn.png";
                    Tiles[pawn.Position] = $"\\Images\\PawnImages\\imageName";
                }
                else if (pawn.State == Pawn.PawnState.AlmostFinished)
                {
                    HighlightAlmostFinishedAvailablePawns(CurrentPlayerTurn, pawn.Position);
                }
            }

            if (diceValue == 6)
            {
                HighlightBaseAvailablePawns(CurrentPlayerTurn);
            }


        }

        private void HighlightAlmostFinishedAvailablePawns(Player CurrentPlayerTurn, byte position)
        {
            if (CurrentPlayerTurn.Color == PlayerColor.Blue)
            {
                BluePath[position] = "\\Images\\PawnImages\\blueHighlightedPawn.png";
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                GreenPath[position] = "\\Images\\PawnImages\\greenHighlightedPawn.png";
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                RedPath[position] = "\\Images\\PawnImages\\redHighlightedPawn.png";
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Yellow)
            {
                YellowPath[position] = "\\Images\\PawnImages\\yellowHighlightedPawn.png";
            }
        }

        private void HighlightBaseAvailablePawns(Player CurrentPlayerTurn)
        {
            if (CurrentPlayerTurn.Color == PlayerColor.Blue)
            {
                for (int i = 0; i < BlueBase.Count; i++)
                {
                    if (BlueBase[i] == "\\Images\\PawnImages\\bluePawn.png")
                    {
                        BlueBase[i] = "\\Images\\PawnImages\\blueHighlightedPawn.png";
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                for (int i = 0; i < GreenBase.Count; i++)
                {
                    if (GreenBase[i] == "\\Images\\PawnImages\\greenPawn.png")
                    {
                        GreenBase[i] = "\\Images\\PawnImages\\greenHighlightedPawn.png";
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                for (int i = 0; i < RedBase.Count; i++)
                {
                    if (RedBase[i] == "\\Images\\PawnImages\\redPawn.png")
                    {
                        RedBase[i] = "\\Images\\PawnImages\\redHighlightedPawn.png";
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Yellow)
            {
                for (int i = 0; i < YellowBase.Count; i++)
                {
                    if (YellowBase[i] == "\\Images\\PawnImages\\yellowPawn.png")
                    {
                        YellowBase[i] = "\\Images\\PawnImages\\yellowHighlightedPawn.png";
                    }
                }
            }
        }

        public void NotifyDiceRolled(byte diceValue)
        {
            _isDiceEanbled = false;
            ChangeDiceValue(diceValue);
            ShowAvailableMoves(diceValue);
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

        private ICommand _pawnBaseSelected;
        public ICommand PawnBaseSelectedCommand
        {
            get
            {
                if (_pawnBaseSelected == null)
                    _pawnBaseSelected = new RelayCommand(PawnBaseSelected);
                return _pawnBaseSelected;
            }
        }

        private void PawnBaseSelected(object parameter)
        {

        }

        private ICommand _pawnPathSelected;
        public ICommand PawnPathSelectedCommand
        {
            get
            {
                if (_pawnPathSelected == null)
                    _pawnPathSelected = new RelayCommand(PawnPathSelected);
                return _pawnPathSelected;
            }
        }

        private void PawnPathSelected(object parameter)
        {

        }
        #endregion

    }
}
