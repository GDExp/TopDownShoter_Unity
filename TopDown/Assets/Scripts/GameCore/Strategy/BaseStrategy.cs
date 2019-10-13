using GameCore.StateMachine;

namespace GameCore.Strategy
{
    class BaseStrategy
    {
        protected IStateMachine stateMachine;
        protected bool isCombat;

        public BaseStrategy(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void CombatTransition()
        {
            isCombat = !isCombat;
        }
    }
}
