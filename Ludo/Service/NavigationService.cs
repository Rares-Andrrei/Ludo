using Ludo.View;
using Ludo.ViewModels;
using System.Windows.Controls;

namespace Ludo.Service
{
    public class NavigationService : BaseVM
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void NavigateTo(UserControl view)
        {
            CurrentView = view;
        }
    }
}
