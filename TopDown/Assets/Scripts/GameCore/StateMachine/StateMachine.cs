using System;
using System.Collections.Generic;

namespace GameCore.StateMachine
{
    public interface IStateMachine
    {
        void Work();
        void ChangeState(Type keyState);
        Type GetTypeCurrentState();
    }

    public class StateMachine<T> : IStateMachine
        where T : class
    {
        public readonly T owner;
        private State<T> _currentState;
        private readonly Dictionary<Type, State<T>> _allStates;
        

        public StateMachine(T owner, Func<object> func)
        {
            this.owner = owner;
            _currentState = null;
            _allStates = func?.Invoke() as Dictionary<Type,State<T>>;
            ChangeState(typeof(Idle));
        }

        public void ChangeState(Type keyState)
        {
            if (keyState == _currentState?.GetType()) return;
            _currentState?.ExitState();
            _currentState = _allStates[keyState];
            _currentState.EnterState();
        }

        public void Work()
        {
            UpdateStatemachine();
        }

        private void UpdateStatemachine()
        {
            var stateType = _currentState?.UpdateState();
            if (stateType != null && stateType != _currentState?.GetType()) ChangeState(stateType);
        }

        public Type GetTypeCurrentState()
        {
            return _currentState?.GetType();
        }
    }
}