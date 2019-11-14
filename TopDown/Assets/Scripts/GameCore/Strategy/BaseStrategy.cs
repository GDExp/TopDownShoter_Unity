using GameCore.StateMachine;

namespace GameCore.Strategy
{
    class BaseStrategy<T>: IStrategy
        where T: Character.AbstractCharacter
    {
        protected T owner;
        protected IStateMachine stateMachine;

        public BaseStrategy(T owner)
        {
            this.owner = owner;
            stateMachine = owner.StateMachine;
        }

        public virtual void DoStrategy()
        {

        }
    }
}
