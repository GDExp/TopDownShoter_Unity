using System;
using System.Collections.Generic;

namespace GameCore.StateMachine
{
    public interface IStateMachine
    {
        void Work();
        void ChangeState(Type keyState);
    }

    public class StateMachine<T> : IStateMachine
        where T : class
    {
        public readonly T owner;
        public State<T> currentState { get; private set; }
        private readonly Dictionary<Type, State<T>> _allStates;
        

        public StateMachine(T owner, Func<object> func)
        {
            this.owner = owner;
            currentState = null;
            _allStates = func?.Invoke() as Dictionary<Type,State<T>>;
            ChangeState(typeof(Idle));
        }

        public void ChangeState(Type keyState)
        {
            if (keyState == currentState?.GetType()) return;
            currentState?.ExitState();
            currentState = _allStates[keyState];
            currentState.EnterState();
        }

        public void Work()
        {
            UpdateStatemachine();
        }

        private void UpdateStatemachine()
        {
            var stateType = currentState?.UpdateState();
            if (stateType != null && stateType != currentState?.GetType()) ChangeState(stateType);
        }
    }
}