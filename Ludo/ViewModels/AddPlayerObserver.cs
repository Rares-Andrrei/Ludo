using Ludo.Service;
using Ludo_Backend.Model;
using System;

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
