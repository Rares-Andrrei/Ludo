using Ludo.Service;
using System.Windows;

namespace Ludo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(NavigationService navigationService)
        {
            DataContext = navigationService;
            InitializeComponent();
        }
    }
}
