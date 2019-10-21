using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class Attack : State<AbstractCharacter>
    {
        private readonly StatusController _status;
        public Attack(AbstractCharacter owner) : base(owner)
        {
            _status = owner.GetStatusController() as StatusController;
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Attack enter!", DebugColor.green);
            SendAttackCommand();
        }

        public override System.Type UpdateState()
        {
            return GetType();
        }

        public override void ExitState()
        {
            _status.SetReloadTime(Time.time);
            CustomDebug.LogMessage("Attack exite!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }

        private void SendAttackCommand()
        {
            var attackValue = Mathf.RoundToInt(Random.Range(0f, 1f));//todo переопределять доступные значения для вида аттака
            ICommand command = new AttackAnimationCommand(owner, GetType(), attackValue, true);
            command.Execute();
        }
    }
}
