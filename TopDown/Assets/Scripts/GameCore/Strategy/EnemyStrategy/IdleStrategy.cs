using System.Collections;
using UnityEngine;
using Character;
using GameCore.StateMachine;


namespace GameCore.Strategy
{
    class IdleStrategy : BasicEnemyStategy
    {
        private Vector3 randomPoint;
        private float timer;
        private float distance;
        private bool isPatrol;

        public IdleStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
            timer = Time.time + Random.Range(5f, 15f);
            isPatrol = false;
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            if (Time.time >= timer && !isPatrol) StartMoveToPoint();
            CheckDistanceToPoint();
            if (distance < navigationController.GetAgentStopDistance() && isPatrol) EndMoveToPoint();
            stateMachine.Work();
        }


        private void StartMoveToPoint()
        {
            var point = Random.insideUnitCircle * Random.Range(15f, 30f);
            randomPoint = enemy.startPosition + new Vector3(point.x, 0f, point.y);
            navigationController.SetCurrentPoint(randomPoint);
            stateMachine.ChangeState(typeof(Move));
            isPatrol = true;
        }

        private void CheckDistanceToPoint()
        {
            if (statusController.isHunting || !isPatrol) return;
            distance = (enemyTransform.position - randomPoint).magnitude;
        }

        private void EndMoveToPoint()
        {
            isPatrol = false;
            timer = Time.time + Random.Range(5f, 15f);
            stateMachine.ChangeState(typeof(Idle));
        }
    }
}
