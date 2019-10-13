using System;

namespace Observer
{
    public interface ISubject
    {
        void Subscribe(Type msgType, IObserver observer);
        void Unsubscribe(Type msgType, IObserver observer);
        void Notify(Type msgType);
    }
}
