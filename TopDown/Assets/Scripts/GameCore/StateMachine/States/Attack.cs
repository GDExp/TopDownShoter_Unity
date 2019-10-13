using UnityEngine;
using Character;


namespace GameCore.StateMachine
{
    class Attack : State<AbstractCharacter>
    {
        private float attackValue;

        public Attack(AbstractCharacter owner) : base(owner)
        {
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Attack enter!", DebugColor.green);
            attackValue = Mathf.RoundToInt(Random.Range(0f, 1f));//todo переопределять доступные значения для вида аттака
            ICommand command = new AttackAnimationCommand(owner, GetType(), attackValue, true);
            command.Execute();
        }

        public override System.Type UpdateState()
        {
            return GetType();
        }

        public override void ExitState()
        {
            CustomDebug.LogMessage("Attack exite!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }
    }
}
