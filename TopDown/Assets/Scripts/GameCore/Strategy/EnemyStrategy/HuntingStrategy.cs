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
            if (!owner.StatusController.isHunting)
            {
                ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.RunSpeed);
                speedCMD.Execute();
            }
            if (owner.StatusController.isCombat) return;
            SetPlayerPosition();
        }

        private void SetPlayerPosition()
        {
            if (enemy.isSmart) owner.NavigationController.SetCurrentPoint(enemy.targetTransform.position + enemy.targetTransform.forward * enemy.targetSpeed / 2f);
            else owner.NavigationController.SetCurrentPoint(enemy.targetTransform.position + _randomCirclePoint);
            if (owner.StatusController.isRetreat) owner.StatusController.isRetreat = false;
            owner.StatusController.isHunting = true;
            stateMachine.ChangeState(typeof(Move));
        }
    }
}
