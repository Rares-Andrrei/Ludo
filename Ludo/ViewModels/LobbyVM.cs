using Ludo.Service;
using Ludo.View;
using Ludo.ViewModels.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Ludo.ViewModels
{
    public class LobbyVM
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string Player3Name { get; set; }
        public string Player4Name { get; set; }

        private readonly NavigationService _navigationService;
        private readonly ServiceCollection _serviceCollection;

        public LobbyVM(ServiceCollection serviceCollection, NavigationService navigationService)
        {
            _navigationService = navigationService;
            _serviceCollection = serviceCollection;
        }

        private ICommand _startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (_startGameCommand == null)
                    _startGameCommand = new RelayCommand(StartGame);
                return _startGameCommand;
            }
        }

        private List<string> GetPlayerNames()
        {
            List<string> playerNames = new List<string>();

            if (!string.IsNullOrEmpty(Player1Name))
            {
                playerNames.Add(Player1Name);
            }
            if (!string.IsNullOrEmpty(Player2Name))
            {
                playerNames.Add(Player2Name);
            }
            if (!string.IsNullOrEmpty(Player3Name))
            {
                playerNames.Add(Player3Name);
            }
            if (!string.IsNullOrEmpty(Player4Name))
            {
                playerNames.Add(Player4Name);
            }

            return playerNames;
        }

        private void StartGame(object parameter)
        {
            List<string> playerNames = GetPlayerNames();

            if (playerNames.Count < 2)
            {
                MessageBox.Show("You need at least 2 players to start the game.");
                return;
            }

            BoardPage boardPage = _serviceCollection.GetService<BoardPage>();
            boardPage.DataContext = _serviceCollection.GetService<BoardVM>();
            ((BoardVM)boardPage.DataContext).InitializeGame(playerNames);

            _navigationService.NavigateTo(boardPage);
        }
    }
}
