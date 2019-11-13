using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class Attack : State<AbstractCharacter>
    {
        private readonly StatusController _status;
        private readonly CombatController<AbstractCharacter> _combatController;

        public Attack(AbstractCharacter owner) : base(owner)
        {
            _status = owner.GetStatusController() as StatusController;
            _combatController = owner.GetCombatController() as CombatController<AbstractCharacter>;
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
            float attackValue = 0f;
            bool rangeAttack = false;
            if(_combatController.currentAttackType == AttackType.Range)
            {
                attackValue = (owner is PlayerCharacter) ? 1f : 0f;//TODO переделать!
                rangeAttack = true;
            }
            else
            {
                attackValue = Mathf.RoundToInt(Random.Range(0f, 1f));//TODO переделать!
            }

            ICommand command = new AttackAnimationCommand(owner, GetType(), attackValue, attackStatus: true, rangeStatus: rangeAttack);
            command.Execute();
        }
    }
}
