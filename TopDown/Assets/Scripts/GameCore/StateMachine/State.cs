using System;

namespace GameCore.StateMachine
{
     public abstract class State<T>
        where T : class
    {
        protected readonly T owner;

        public  State(T owner)
        {
            this.owner = owner;
        }

        public abstract void EnterState();
        public abstract Type UpdateState();
        public abstract void ExitState();
    }
}
