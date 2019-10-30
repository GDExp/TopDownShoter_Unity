using System;

namespace GameCore
{
    class PatternCommand<T> : ICommand
        where T: class
    {
        protected readonly T _invoker;

        public PatternCommand(T invoker)
        {
            _invoker = invoker;
        }

        public virtual void Execute()
        {

        }
    }
}
