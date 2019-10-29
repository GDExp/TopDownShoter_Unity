using UnityEngine;
using GameCore;
using GameCore.StateMachine;

namespace Character
{
    class PlayerCharacter: AbstractCharacter
    {
        public bool showHP;//test

        protected override void SetupCharacter()
        {
            base.SetupCharacter();
        }

        public override void UpdateCharacter()
        {
            base.UpdateCharacter();
            stateMachine.Work();
            if (showHP) statusController.RefreshHelth(ref hp_test);//test
            if (statusController.isCombat) return;

            if (GameController.Instance.xValue != 0 || GameController.Instance.zValue != 0) stateMachine.ChangeState(typeof(Move));
            else stateMachine.ChangeState(typeof(Idle));
            if (GameController.Instance.attackKey && statusController.CheckReloadTime(Time.time)) stateMachine.ChangeState(typeof(Attack));
            
        }
    }
}
