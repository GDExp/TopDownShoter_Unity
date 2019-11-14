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
            if(!owner.StatusController.isRetreat) StartMove();
            CheckDistanceToPoint();
            if(distance < owner.NavigationController.GetAgentStopDistance()) EndMove();
        }
        
        private void StartMove()
        {
            ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.ExtraRunSpeed);
            speedCMD.Execute();

            owner.StatusController.isRetreat = true;
            owner.NavigationController.SetCurrentPoint(enemy.startPosition);
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

            owner.StatusController.isHunting = false;
            owner.StatusController.isRetreat = false;
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
