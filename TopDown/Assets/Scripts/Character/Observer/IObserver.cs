using System;

namespace Observer
{
    public interface IObserver
    {
        void UpdateObserver(Type subjectTypeCallback);
    }
}
