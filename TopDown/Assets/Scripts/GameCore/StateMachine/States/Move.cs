using System;
using GameCore.Strategy;
using Character;

namespace GameCore.StateMachine
{
    class Move : State <AbstractCharacter>
    {
        private IStrategy _localStrategy;
        public Move(AbstractCharacter owner) : base(owner)
        {
            _localStrategy = (owner as PlayerCharacter) ? new MovePlayerStrategy(owner) : null;//todo - add ENEMY move stategy
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Move enter!", DebugColor.green);
            ICommand command = new ChangeAnimationCommand(owner, GetType(), 1f);
            command.Execute();
        }

        public override Type UpdateState()
        {
            //TO DO release Player and Enemy
            _localStrategy.DoStrategy();
            return GetType();
        }

        public override void ExitState()
        {
            CustomDebug.LogMessage("Move exit!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }
    }
}
