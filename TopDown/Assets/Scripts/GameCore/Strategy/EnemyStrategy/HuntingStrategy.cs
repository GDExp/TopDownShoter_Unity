using Character;
using GameCore.StateMachine;
using UnityEngine;

namespace GameCore.Strategy
{
    class HuntingStrategy : BasicEnemyStategy
    {
        private Vector3 _randomCirclePoint;
        
        public HuntingStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            if (!statusController.isHunting)
            {
                ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.RunSpeed);
                speedCMD.Execute();
            }
            if (statusController.isCombat) return;
            SetPlayerPosition();
        }

        private void SetPlayerPosition()
        {
            if (enemy.isSmart) navigationController.SetCurrentPoint(enemy.targetTransform.position + enemy.targetTransform.forward * enemy.targetSpeed / 2f);
            else navigationController.SetCurrentPoint(enemy.targetTransform.position + _randomCirclePoint);
            if (statusController.isRetreat) statusController.isRetreat = false; 
            statusController.isHunting = true;
            stateMachine.ChangeState(typeof(Move));
        }
    }
}
