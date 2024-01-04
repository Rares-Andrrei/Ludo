using Ludo.Service;
using Ludo.View;
using Ludo.ViewModels.Commands;
using System.Windows.Input;

namespace Ludo.ViewModels
{
    public class StartingPageVM
    {
        private readonly NavigationService navigationService;
        private readonly ServiceCollection serviceCollection;

        public StartingPageVM(NavigationService navigationService, ServiceCollection serviceCollection)
        {
            this.navigationService = navigationService;
            this.serviceCollection = serviceCollection;
        }

        private ICommand _startCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_startCommand == null)
                    _startCommand = new RelayCommand(Start);
                return _startCommand;
            }
        }

        private void Start(object parameter)
        {
            navigationService.NavigateTo(serviceCollection.GetService<LobbyPage>());
        }
    }
}
