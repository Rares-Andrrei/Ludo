using Ludo.Service;
using Ludo.View;
using Ludo.ViewModels.Commands;
using Ludo_Backend.Functionaity;
using Ludo_Backend.Functionaity.Interfaces;
using Ludo_Backend.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static Ludo_Backend.Functionaity.Player;

namespace Ludo.ViewModels
{
    public class BoardVM : BaseVM, IGameEngineOberver
    {
        private const byte TilesNr = 52;
        private const byte PawnsNr = 4;
        private const byte pathTilesNumber = 5;
        private const string defaultDice = "\\Images\\DiceImages\\dice.jpeg";
        private const string redPawn = "\\Images\\PawnImages\\redPawn.png";
        private const string bluePawn = "\\Images\\PawnImages\\bluePawn.png";
        private const string greenPawn = "\\Images\\PawnImages\\greenPawn.png";
        private const string yellowPawn = "\\Images\\PawnImages\\yellowPawn.png";
        private const string highlightedRed = "\\Images\\PawnImages\\redHighlightedPawn.png";
        private const string highlightedBlue = "\\Images\\PawnImages\\blueHighlightedPawn.png";
        private const string highlightedGreen = "\\Images\\PawnImages\\greenHighlightedPawn.png";
        private const string highlightedYellow = "\\Images\\PawnImages\\yellowHighlightedPawn.png";

        private readonly NavigationService _navigationService;
        private readonly ServiceCollection _serviceCollection;

        public BoardVM(ServiceCollection serviceCollection, NavigationService navigationService)
        {
            _navigationService = navigationService;
            _serviceCollection = serviceCollection;
        }

        private IGameEngine _gameEngine;
        private ObservableCollection<string> _tiles;
        public ObservableCollection<string> Tiles
        {
            get { return _tiles; }
            set
            {
                _tiles = value;
                OnPropertyChanged();
            }
        }
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
        private ObservableCollection<string> _redPath;
        public ObservableCollection<string> RedPath
        {
            get { return _redPath; }
            set
            {
                _redPath = value;
                OnPropertyChanged();
            }
        }
        private byte _redPlayerScore;
        public byte RedPlayerScore
        {
            get { return _redPlayerScore; }
            set
            {
                _redPlayerScore = value;
                OnPropertyChanged();
            }
        }
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
        private ObservableCollection<string> _bluePath;
        public ObservableCollection<string> BluePath
        {
            get { return _bluePath; }
            set
            {
                _bluePath = value;
                OnPropertyChanged();
            }
        }
        private byte _bluePlayerScore;
        public byte BluePlayerScore
        {
            get { return _bluePlayerScore; }
            set
            {
                _bluePlayerScore = value;
                OnPropertyChanged();
            }
        }
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
        private ObservableCollection<string> _greenPath;
        public ObservableCollection<string> GreenPath
        {
            get { return _greenPath; }
            set
            {
                _greenPath = value;
                OnPropertyChanged();
            }
        }
        private byte _greenPlayerScore;
        public byte GreenPlayerScore
        {
            get { return _greenPlayerScore; }
            set
            {
                _greenPlayerScore = value;
                OnPropertyChanged();
            }
        }
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
        private ObservableCollection<string> _yellowPath;
        public ObservableCollection<string> YellowPath
        {
            get { return _yellowPath; }
            set
            {
                _yellowPath = value;
                OnPropertyChanged();
            }
        }
        private byte _yellowPlayerScore;
        public byte YellowPlayerScore
        {
            get { return _yellowPlayerScore; }
            set
            {
                _yellowPlayerScore = value;
                OnPropertyChanged();
            }
        }
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

        private byte lastBasePawnClicked;

        private void InitializeViewModel()
        {
            Tiles = new ObservableCollection<string>(Enumerable.Repeat<string>("", TilesNr).ToList());
            if (RedPlayerName != null)
            {
                RedBase = new ObservableCollection<string>(Enumerable.Repeat<string>(redPawn, PawnsNr).ToList());
                RedPath = new ObservableCollection<string>(Enumerable.Repeat<string>("", pathTilesNumber).ToList());
                RedPlayerScore = 0;
            }
            if (BluePlayerName != null)
            {
                BlueBase = new ObservableCollection<string>(Enumerable.Repeat<string>(bluePawn, PawnsNr).ToList());
                BluePath = new ObservableCollection<string>(Enumerable.Repeat<string>("", pathTilesNumber).ToList());
                BluePlayerScore = 0;
            }
            if (GreenPlayerName != null)
            {
                GreenBase = new ObservableCollection<string>(Enumerable.Repeat<string>(greenPawn, PawnsNr).ToList());
                GreenPath = new ObservableCollection<string>(Enumerable.Repeat<string>("", pathTilesNumber).ToList());
                GreenPlayerScore = 0;
            }
            if (YellowPlayerName != null)
            {
                YellowBase = new ObservableCollection<string>(Enumerable.Repeat<string>(yellowPawn, PawnsNr).ToList());
                YellowPath = new ObservableCollection<string>(Enumerable.Repeat<string>("", pathTilesNumber).ToList());
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
            CurrentPlayerTurn = _gameEngine.CurrentPlayerTurn;
            _isDiceEanbled = true;
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
                MessageBox.Show($"No available moves for player: {CurrentPlayerTurn.Name}", "Info", MessageBoxButton.OK);
                MoveMade();
                return;
            }

            foreach (Pawn pawn in availablePawns)
            {
                if (pawn.State == Pawn.PawnState.InPlay)
                {
                    string imageName = $"{CurrentPlayerTurn.Color.ToString().ToLower()}HighlightedPawn.png";
                    Tiles[pawn.Position] = $"\\Images\\PawnImages\\{imageName}";
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
                BluePath[position] = highlightedBlue;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                GreenPath[position] = highlightedGreen;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                RedPath[position] = highlightedRed;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Yellow)
            {
                YellowPath[position] = highlightedYellow;
            }
        }
        private void HighlightBaseAvailablePawns(Player CurrentPlayerTurn)
        {
            if (CurrentPlayerTurn.Color == PlayerColor.Blue)
            {
                for (int i = 0; i < BlueBase.Count; i++)
                {
                    if (BlueBase[i] == bluePawn)
                    {
                        BlueBase[i] = highlightedBlue;
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                for (int i = 0; i < GreenBase.Count; i++)
                {
                    if (GreenBase[i] == greenPawn)
                    {
                        GreenBase[i] = highlightedGreen;
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                for (int i = 0; i < RedBase.Count; i++)
                {
                    if (RedBase[i] == redPawn)
                    {
                        RedBase[i] = highlightedRed;
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

        private void RemoveHighlightBaseAvailablePawns(Player CurrentPlayerTurn)
        {
            if (CurrentPlayerTurn.Color == PlayerColor.Blue)
            {
                for (int i = 0; i < BlueBase.Count; i++)
                {
                    if (BlueBase[i] == highlightedBlue)
                    {
                        BlueBase[i] = bluePawn;
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                for (int i = 0; i < GreenBase.Count; i++)
                {
                    if (GreenBase[i] == highlightedGreen)
                    {
                        GreenBase[i] = greenPawn;
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                for (int i = 0; i < RedBase.Count; i++)
                {
                    if (RedBase[i] == highlightedRed)
                    {
                        RedBase[i] = redPawn;
                    }
                }
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Yellow)
            {
                for (int i = 0; i < YellowBase.Count; i++)
                {
                    if (YellowBase[i] == highlightedYellow)
                    {
                        YellowBase[i] = yellowPawn;
                    }
                }
            }
        }
        private void RemoveHighlightAlmostFinishedAvailablePawns(Player CurrentPlayerTurn, byte position)
        {
            if (CurrentPlayerTurn.Color == PlayerColor.Blue)
            {
                BluePath[position] = bluePawn;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Green)
            {
                GreenPath[position] = greenPawn;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Red)
            {
                RedPath[position] = redPawn;
            }
            else if (CurrentPlayerTurn.Color == PlayerColor.Yellow)
            {
                YellowPath[position] = yellowPawn;
            }
        }
        private void RemoveHighlihtFromAllPawns()
        {
            List<Pawn> availablePawns = _gameEngine.AvailablePawnsToMoveForCurrentPlayer();

            foreach (Pawn pawn in availablePawns)
            {
                if (pawn.State == Pawn.PawnState.InPlay)
                {
                    string imageName = $"{CurrentPlayerTurn.Color.ToString().ToLower()}Pawn.png";
                    Tiles[pawn.Position] = $"\\Images\\PawnImages\\{imageName}";
                }
                else if (pawn.State == Pawn.PawnState.AlmostFinished)
                {
                    RemoveHighlightAlmostFinishedAvailablePawns(CurrentPlayerTurn, pawn.Position);
                }
            }
            RemoveHighlightBaseAvailablePawns(CurrentPlayerTurn);
        }
        public void MoveMade()
        {
            RemoveHighlihtFromAllPawns();
            _gameEngine.FinishedTurn();
            CurrentPlayerTurn = _gameEngine.CurrentPlayerTurn;
            _isDiceEanbled = true;
            DiceImage = defaultDice;
        }

        #region Notify

        public void NotifyDiceRolled(byte diceValue)
        {
            _isDiceEanbled = false;
            ChangeDiceValue(diceValue);
            ShowAvailableMoves(diceValue);
        }

        public void NotifyInPlayPawnMoveMade(byte pawnPosition, byte destination)
        {
            RemoveHighlihtFromAllPawns();
            Tiles[pawnPosition] = "";
            Tiles[destination] = $"\\Images\\PawnImages\\{CurrentPlayerTurn.Color.ToString().ToLower()}Pawn.png";
            MoveMade();
        }

        public void NotifyPawnReleasedFromBase(PlayerColor color, byte newPos)
        {
            MoveMade();
            switch (color)
            {
                case PlayerColor.Red:
                    RedBase[lastBasePawnClicked] = "";
                    Tiles[newPos] = redPawn;
                    break;
                case PlayerColor.Blue:
                    BlueBase[lastBasePawnClicked] = "";
                    Tiles[newPos] = bluePawn;
                    break;
                case PlayerColor.Green:
                    GreenBase[lastBasePawnClicked] = "";
                    Tiles[newPos] = greenPawn;
                    break;
                case PlayerColor.Yellow:
                    YellowBase[lastBasePawnClicked] = "";
                    Tiles[newPos] = yellowPawn;
                    break;
            }
        }

        public void NotifyScoreCanged(PlayerColor color, byte score)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    RedPlayerScore = score;
                    break;
                case PlayerColor.Blue:
                    BluePlayerScore = score;
                    break;
                case PlayerColor.Green:
                    GreenPlayerScore = score;
                    break;
                case PlayerColor.Yellow:
                    YellowPlayerScore = score;
                    break;
            }
        }

        public void NotifyGameFinished(PlayerColor winner)
        {
            switch (winner)
            {
                case PlayerColor.Red:
                    MessageBox.Show($"{RedPlayerName} player won", "Finish", MessageBoxButton.OK);
                    break;
                case PlayerColor.Blue:
                    MessageBox.Show($"{BluePlayerName} player won", "Finish", MessageBoxButton.OK);
                    break;
                case PlayerColor.Green:
                    MessageBox.Show($"{GreenPlayerName} player won", "Finish", MessageBoxButton.OK);
                    break;
                case PlayerColor.Yellow:
                    MessageBox.Show($"{YellowPlayerName} player won", "Finish", MessageBoxButton.OK);
                    break;
            }

            StartingPage startPage = _serviceCollection.GetService<StartingPage>();
            _navigationService.NavigateTo(startPage);

        }

        public void NotifyAlmostFinishedPawnMoveMade(Player.PlayerColor playerColor, byte pawnPosition, byte destination)
        {
            switch (playerColor)
            {
                case PlayerColor.Red:
                    RedPath[pawnPosition] = "";
                    RedPath[destination] = redPawn;
                    break;
                case PlayerColor.Blue:
                    BluePath[pawnPosition] = "";
                    BluePath[destination] = bluePawn;
                    break;
                case PlayerColor.Green:
                    GreenPath[pawnPosition] = "";
                    GreenPath[destination] = greenPawn;
                    break;
                case PlayerColor.Yellow:
                    YellowPath[pawnPosition] = "";
                    YellowPath[destination] = yellowPawn;
                    break;
            }
            MoveMade();
        }

        public void NotifyInPlayToAlmostFinishedPawnMoveMade(PlayerColor playerColor, byte pawnPosition, byte destination)
        {
            Tiles[pawnPosition] = "";

            switch (playerColor)
            {
                case PlayerColor.Red:
                    RedPath[destination] = redPawn;
                    break;
                case PlayerColor.Blue:
                    BluePath[destination] = bluePawn;
                    break;
                case PlayerColor.Green:
                    GreenPath[destination] = greenPawn;
                    break;
                case PlayerColor.Yellow:
                    YellowPath[destination] = yellowPawn;
                    break;
            }
            MoveMade();
        }

        public void NotifyPawnFinished(Pawn.PawnState pawnState, PlayerColor playerColor, byte pawnPosition)
        {
            if (pawnState == Pawn.PawnState.AlmostFinished)
            {
                switch (playerColor)
                {
                    case PlayerColor.Red:
                        RedPath[pawnPosition] = "";
                        break;
                    case PlayerColor.Blue:
                        BluePath[pawnPosition] = "";
                        break;
                    case PlayerColor.Green:
                        GreenPath[pawnPosition] = "";
                        break;
                    case PlayerColor.Yellow:
                        YellowPath[pawnPosition] = "";
                        break;
                }
            }
            else
            {
                Tiles[pawnPosition] = "";
            }
            MoveMade();
        }

        #endregion

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
            if (_isDiceEanbled == true)
            {
                return;
            }
            _gameEngine.MoveInPlayPawn(byte.Parse(parameter as string));
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
            if (_isDiceEanbled == true)
            {
                return;
            }

            string[] parts = (parameter as string).Split(' ');

            if (parts[1] == CurrentPlayerTurn.Color.ToString())
            {
                lastBasePawnClicked = byte.Parse(parts[0]);
                _gameEngine.ReleasePawnFromBaseCurrentPlayer();
            }
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
            if (_isDiceEanbled == true)
            {
                return;
            }
            string[] parts = (parameter as string).Split(' ');

            if (parts[1] == CurrentPlayerTurn.Color.ToString())
            {
                _gameEngine.MoveAlmostFinishedPawn(Player.StringToPlayerColor(parts[1]), byte.Parse(parts[0]));
            }
        }

        #endregion

    }
}
