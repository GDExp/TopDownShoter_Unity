using UnityEngine;
using GameCore.Strategy;
using GameCore.StateMachine;
using Character;

namespace GameCore.Strategy
{
    class ReturnStrategy : BasicEnemyStategy
    {
        private float distance;

        public ReturnStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            if(!statusController.isRetreat) StartMove();
            CheckDistanceToPoint();
            if(distance < navigationController.GetAgentStopDistance()) EndMove();
        }
        
        private void StartMove()
        {
            ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.ExtraRunSpeed);
            speedCMD.Execute();

            statusController.isRetreat = true;
            navigationController.SetCurrentPoint(enemy.startPosition);
            stateMachine.ChangeState(typeof(Move));
        }

        private void CheckDistanceToPoint()
        {
            distance = (enemyTransform.position - enemy.startPosition).magnitude;
        }

        private void EndMove()
        {
            ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.NormalSpeed);
            speedCMD.Execute();

            statusController.isHunting = false;
            statusController.isRetreat = false;
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
