using System;

namespace Ludo_Backend.Observer
{
    public interface IGameEngineSubject
    {
        void Attach(IGameEngineOberver observer);
        void Detach(IGameEngineOberver observer);
    }
}
