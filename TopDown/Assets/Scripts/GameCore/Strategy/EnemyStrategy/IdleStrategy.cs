using UnityEngine;
using Character;
using GameCore.StateMachine;


namespace GameCore.Strategy
{
    class IdleStrategy : BasicEnemyStategy
    {
        private Vector3 _randomPoint;
        private float _timerHeal;
        private float _timer;
        private float _distance;
        private bool isMovePoint;

        public IdleStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
            _timer = Time.time + Random.Range(5f, 15f);
            isMovePoint = false;
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            IdleHealing();
            if (Time.time >= _timer && !isMovePoint) StartMoveToPoint();
            CheckDistanceToPoint();
            if (_distance < navigationController.GetAgentStopDistance() && isMovePoint) EndMoveToPoint();
        }
        
        private void IdleHealing()
        {
            if (Time.time < _timerHeal && !statusController.CheckCurrentHealthToLimit(HealthStatus.MaxHealth)) return;
            _timerHeal = Time.time + 5f;
            var healingCommand = new HealingCommand(enemy, enemy.healingPower);
            healingCommand.Execute();
        }

        private void StartMoveToPoint()
        {
            var point = Random.insideUnitCircle * enemy.patrolDistance;
            _randomPoint = enemy.startPosition + new Vector3(point.x, 0f, point.y);
            navigationController.SetCurrentPoint(_randomPoint);
            stateMachine.ChangeState(typeof(Move));
            isMovePoint = true;
        }

        private void CheckDistanceToPoint()
        {
            if (statusController.isHunting || !isMovePoint) return;
            _distance = (enemyTransform.position - _randomPoint).magnitude;
        }

        private void EndMoveToPoint()
        {
            isMovePoint = false;
            _timer = Time.time + Random.Range(5f, 15f);
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
