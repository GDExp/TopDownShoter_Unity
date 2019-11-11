using System;
using Character;

namespace GameCore.StateMachine
{
    class Dead : State<AbstractCharacter>
    {
        public Dead(AbstractCharacter owner) : base(owner)
        {
        }

        public override void EnterState()
        {
            CustomDebug.LogMessage("Dead enter!", DebugColor.green);
            ICommand deadCMD = new DeadAnimationCommand(owner, GetType());
            deadCMD.Execute();
        }

        public override Type UpdateState()
        {
            return GetType();
        }

        public override void ExitState()
        {
            CustomDebug.LogMessage("Dead exit!", DebugColor.green);
        }
    }
}
