using System;
using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class Idle : State<AbstractCharacter>
    {
        private float timerToFlip;

        public Idle(AbstractCharacter owner) : base(owner)
        {
        }

        public override void EnterState()
        {
            timerToFlip = ResetTimer();
            CustomDebug.LogMessage("Idle enter!", DebugColor.green);
        }

        public override Type UpdateState()
        {
            if(Time.time >= timerToFlip)
            {
                ICommand command = new ChangeAnimationCommand(owner, GetType(), 1f);//TODO подумать как отвязаться от фикс значеий!
                command.Execute();
                timerToFlip = ResetTimer();
            }

            return GetType();
        }

        public override void ExitState()
        {
            CustomDebug.LogMessage("Idle exit!", DebugColor.red);
            ICommand command = new ChangeAnimationCommand(owner, GetType());
            command.Execute();
        }


        private float ResetTimer()
        {
            return Time.time + UnityEngine.Random.Range(5f, 10f);
        }
    }
}
