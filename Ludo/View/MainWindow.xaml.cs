using Ludo.Model;
using Ludo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ludo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player
            {
                Name = inputPlayer.Text
            };
            AddPlayerModel addPlayerModel = new AddPlayerModel();
            AddPlayerObserver addPlayerObserver = new AddPlayerObserver();
            addPlayerObserver.PlayerUpdate += PlayerUpdateHandler;
            addPlayerModel.Attach(addPlayerObserver);
            addPlayerObserver.player1  =player;
            
            
          
            
        }

        private void PlayerUpdateHandler(Player obj)
        {
           notifyPlayer.Text = obj.Name;
        }
    }
}
