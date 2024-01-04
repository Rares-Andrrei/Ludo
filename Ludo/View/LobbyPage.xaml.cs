using Ludo.Service;
using Ludo.ViewModels;
using System.Windows.Controls;

namespace Ludo.View
{
    /// <summary>
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : UserControl
    {
        public LobbyPage(ServiceCollection serviceCollection)
        {
            DataContext = serviceCollection.GetService<LobbyVM>();
            InitializeComponent();
        }
    }
}
