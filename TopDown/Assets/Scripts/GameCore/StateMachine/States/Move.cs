using System;
using Character;

namespace GameCore.StateMachine
{
    class Move : State <AbstractCharacter>
    {
        private readonly StatusController _statusController;
        private AbstractMovementLogic<AbstractCharacter> _movementLogic;


        public Move(AbstractCharacter owner) : base(owner)
        {
            _statusController = owner.GetStatusController() as StatusController;
            if (owner is PlayerCharacter) _movementLogic = new PlayerMovementLogic(owner);
            else _movementLogic = new EnemyMovementLogic(owner);
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Move enter!", DebugColor.green);
            CustomDebug.LogMessage(_statusController.GetSpeedModificator());
            ICommand command = new ChangeAnimationCommand(owner, GetType(), _statusController.GetSpeedModificator());
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
