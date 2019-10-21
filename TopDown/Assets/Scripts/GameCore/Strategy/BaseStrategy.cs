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
            stateMachine = owner.GetStateMachine() as IStateMachine;
        }

        public virtual void DoStrategy()
        {

        }
        //it remove?s
        public void EndStrategy()
        {

        }
    }
}
