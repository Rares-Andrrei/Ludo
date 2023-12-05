using Ludo.Model;
using Ludo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ludo.ViewModels
{
    public class AddPlayerObserver : IObserver
    { 
        public void Update(Player player)
        {

            MessageBox.Show($"Player {player.Name} with {player.Color.ToString()} has been added");

        }
    }
}
