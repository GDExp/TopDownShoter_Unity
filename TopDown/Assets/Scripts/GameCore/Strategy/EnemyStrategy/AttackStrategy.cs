using UnityEngine;
using Character;
using GameCore.StateMachine;

namespace GameCore.Strategy
{
    class AttackStrategy : BasicEnemyStategy
    {
        private readonly Transform _enemy;
        private Transform target;

        public AttackStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
            _enemy = owner.transform;
            target = enemy.targetTransform;//test
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            LookAtTarget();
            if (!statusController.CheckReloadTime(Time.time) || statusController.isCombat) return;
            AttackTarget();
        }
        
        private void LookAtTarget()
        {
            if (stateMachine.GetTypeCurrentState() == typeof(Attack)) return;
            stateMachine.ChangeState(typeof(Idle));
            Vector3 direction = _enemy.position - target.position;
            _enemy.LookAt(target);
        }

        private void AttackTarget()
        {
            stateMachine.ChangeState(typeof(Attack));
        }
    }
}
