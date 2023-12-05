using Ludo.Model;
using Ludo.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.ViewModels
{
    public class AddPlayerModel : ISubject
    {
        List<IObserver> _observers=new List<IObserver>();
      
       
        public void AddPlayer(Player player)
        {

            Notify(player);
        }
     
       public void Notify(Player player)
        {
            foreach(var observer in _observers)
            {
                observer.Update(player);
            }
        }
        public void Attach(IObserver observer)
        {
           _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
           _observers.Remove(observer);
        }
    }
}
