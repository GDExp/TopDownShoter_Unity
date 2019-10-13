using GameCore.StateMachine;
using UnityEngine;


namespace GameCore.Strategy
{
    class PlayerStrategy : BaseStrategy, IStrategy
    {
        public PlayerStrategy(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public void DoStrategy()
        {
            if (!isCombat)
            {
                if (GameController.Instance.xValue != 0 || GameController.Instance.zValue != 0) stateMachine.ChangeState(typeof(Move));
                else stateMachine.ChangeState(typeof(Idle));
                if (Input.GetKeyDown(KeyCode.Space)) stateMachine.ChangeState(typeof(Attack));
            }
            stateMachine.Work();
        }
    }
}
