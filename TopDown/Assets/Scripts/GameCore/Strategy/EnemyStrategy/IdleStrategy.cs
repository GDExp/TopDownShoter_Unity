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
        private bool isPatrol;

        public IdleStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
            _timer = Time.time + Random.Range(5f, 15f);
            isPatrol = false;
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            IdleHealing();
            if (Time.time >= _timer && !isPatrol) StartMoveToPoint();
            CheckDistanceToPoint();
            if (_distance < navigationController.GetAgentStopDistance() && isPatrol) EndMoveToPoint();
        }
        
        private void IdleHealing()
        {
            if (Time.time < _timerHeal && !statusController.CheckCurrentHealthIsMax()) return;
            _timerHeal = Time.time + 5f;
            var healingCommand = new HealingCommand(enemy, enemy.healingPower);
            healingCommand.Execute();
            statusController.RefreshHelth(ref enemy.hp_test);
        }

        private void StartMoveToPoint()
        {
            var point = Random.insideUnitCircle * Random.Range(15f, 30f);
            _randomPoint = enemy.startPosition + new Vector3(point.x, 0f, point.y);
            navigationController.SetCurrentPoint(_randomPoint);
            stateMachine.ChangeState(typeof(Move));
            isPatrol = true;
        }

        private void CheckDistanceToPoint()
        {
            if (statusController.isHunting || !isPatrol) return;
            _distance = (enemyTransform.position - _randomPoint).magnitude;
        }

        private void EndMoveToPoint()
        {
            isPatrol = false;
            _timer = Time.time + Random.Range(5f, 15f);
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
