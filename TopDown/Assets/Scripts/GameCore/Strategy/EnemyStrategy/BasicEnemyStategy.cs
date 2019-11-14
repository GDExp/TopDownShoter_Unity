using UnityEngine;
using Character;

namespace GameCore.Strategy
{
    class BasicEnemyStategy : BaseStrategy<AbstractCharacter>
    {
        protected readonly EnemyCharacter enemy;
        protected readonly Transform enemyTransform;
        private readonly string _status;

        public BasicEnemyStategy(AbstractCharacter owner, string status) : base(owner)
        {
            enemy = owner as EnemyCharacter;
            enemyTransform = owner.transform;
            _status = status;
        }

        public override void DoStrategy()
        {
            stateMachine.Work();
            if (Application.isEditor) ChangeEnemyStatus();
        }

#if UNITY_EDITOR
        private void ChangeEnemyStatus()
        {
            enemy.enemyStatus = _status;
        }
#endif
    }
}
