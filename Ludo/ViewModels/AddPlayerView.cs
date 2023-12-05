using Ludo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.ViewModels
{
    public class AddPlayerView
    {
        
        public Player player { get; set; }

        public AddPlayerView()
        {
            var addPlayerModel = new AddPlayerModel();
            var addPlayerObserver=new AddPlayerObserver();
            addPlayerModel.Attach(addPlayerObserver);
            player = new Player();
            player.Name = "Eu";
            addPlayerModel.AddPlayer();
        }


    }
}
    

