using System;
using Character;

namespace GameCore.StateMachine
{
    class Move : State <AbstractCharacter>
    {
        private AbstractMovementLogic<AbstractCharacter> _movementLogic;

        public Move(AbstractCharacter owner) : base(owner)
        {

            if (owner is PlayerCharacter) _movementLogic = new PlayerMovementLogic(owner);
            else _movementLogic = new EnemyMovementLogic(owner);
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Move enter!", DebugColor.green);
            ICommand command = new ChangeAnimationCommand(owner, GetType(), 1f);
            command.Execute();
        }

        public override Type UpdateState()
        {
            _movementLogic.WorkMovement();
            return GetType();
        }

        public override void ExitState()
        {
            _movementLogic.InteractObstacle();
            CustomDebug.LogMessage("Move exit!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }
    }
}
