namespace Ludo.Service
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(ISubject subject);
    }
}
