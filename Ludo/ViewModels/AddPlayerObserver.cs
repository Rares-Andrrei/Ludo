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
        public AddPlayerModel addPlayer { get; set; }
        private Player _player1;
        public event Action<Player> PlayerUpdate;
        public Player player1
        {
            get
            {
                return _player1;
            }
            set
            {
                _player1 = value;
                Update(_player1);
            }
        }



        public void Update(Player player)
        {

            PlayerUpdate?.Invoke(player);




        }
        public AddPlayerObserver() { }
    }
}
