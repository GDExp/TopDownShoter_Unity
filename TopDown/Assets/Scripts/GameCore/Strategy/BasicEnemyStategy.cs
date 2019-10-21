using UnityEngine;
using Character;

namespace GameCore.Strategy
{
    class BasicEnemyStategy : BaseStrategy<AbstractCharacter>
    {
        protected readonly EnemyCharacter enemy;
        protected readonly Transform enemyTransform;
        protected readonly NavigationController navigationController;
        protected readonly StatusController statusController;
        private readonly string _status;

        public BasicEnemyStategy(AbstractCharacter owner, string status) : base(owner)
        {
            enemy = owner as EnemyCharacter;
            enemyTransform = owner.transform;
            navigationController = owner.GetNavigationController() as NavigationController;
            statusController = owner.GetStatusController() as StatusController;
            _status = status;
        }

        public override void DoStrategy()
        {
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
