using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class Attack : State<AbstractCharacter>
    {
        public Attack(AbstractCharacter owner) : base(owner)
        {
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
            owner.StatusController.SetReloadTime(Time.time);
            CustomDebug.LogMessage("Attack exite!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }

        private void SendAttackCommand()
        {
            float attackValue = 0f;
            bool rangeAttack = false;
            if(owner.CombatController.CurrentAttackType == AttackType.Range)
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
