using Ludo.Service;
using Ludo_Backend.Model;
using System.Collections.Generic;

namespace Ludo.ViewModels
{
    public class AddPlayerModel : ISubject
    {
        List<IObserver> _observers = new List<IObserver>();




        public void Notify(ISubject subject)
        {
            var player = subject as Player;
            foreach (var observer in _observers)
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
