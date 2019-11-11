using UnityEngine;
using GameCore.StateMachine;
using Character;

namespace GameCore.Strategy
{
    class RetreatStrategy : BasicEnemyStategy
    {
        private Vector3 _point;
        private float _healingTimer;
        private float _lastTimeRetreat = 0f;

        public RetreatStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            if (!statusController.isRetreat) SetRetreatPoint();
            else WorkRetreat();
        }

        private void SetRetreatPoint()
        {
            Vector3 direction = (enemy.targetTransform.position + enemyTransform.position).normalized;
            _point = enemyTransform.position + direction * enemy.visionRadius * 1.5f;

            navigationController.SetCurrentPoint(_point);

            ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.ExtraRunSpeed);
            speedCMD.Execute();

            statusController.isRetreat = true;
            statusController.isHunting = false;

            _lastTimeRetreat = Time.time + 120f;
            stateMachine.ChangeState(typeof(Move));
        }

        private void WorkRetreat()
        {
            RefreshRetreatStatus();
            QuickHealing();
            if (!CheckDistanceToPoint()) return;
            StopMoveToPoint();
        }

        private void RefreshRetreatStatus()
        {
            if (!statusController.CheckCurrentHealthToLimit(HealthStatus.MediumHealth)) statusController.isRetreat = false;
            if (statusController.CheckCurrentHealthToLimit(HealthStatus.MediumHealth)) return;
            _point = Vector3.zero;
        }

        private bool CheckDistanceToPoint()
        {
            return (enemyTransform.position - _point).magnitude < navigationController.GetAgentStopDistance();
        }

        private void QuickHealing()
        {
            if (Time.time >= _healingTimer)
            {
                _healingTimer = Time.time + 1.5f;
                var healingCommand = new HealingCommand(enemy, enemy.healingPower);
                healingCommand.Execute();
            }
        }

        private void StopMoveToPoint()
        {
            ICommand speedCMD = new ChangeAnimationSpeedCommand(enemy, SpeedStatus.NormalSpeed);
            speedCMD.Execute();
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
