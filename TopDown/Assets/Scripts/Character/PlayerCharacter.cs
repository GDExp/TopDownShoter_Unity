using UnityEngine;
using GameCore;
using GameCore.StateMachine;

namespace Character
{
    [AddComponentMenu("Characters/Player")]
    public class PlayerCharacter: AbstractCharacter
    {
        public PlayerInputModule playerInput;
        public float attackDistance;//??? EnemyCharacter

        protected override void SetupCharacter()
        {
            base.SetupCharacter();
            attackDistance = navigationController.GetAgentStopDistance();//if only melee attack type
            ICommand speedCMD = new ChangeAnimationSpeedCommand(this, SpeedStatus.RunSpeed);
            speedCMD.Execute();
        }

        public override void UpdateCharacter()
        {
            base.UpdateCharacter();
            if (statusController.isCombat) return;

            if (playerInput.isMove) stateMachine.ChangeState(typeof(Move));
            else stateMachine.ChangeState(typeof(Idle));
            if (playerInput.isAttack && statusController.CheckReloadTime(Time.time)) stateMachine.ChangeState(typeof(Attack));
        }
    }
}
