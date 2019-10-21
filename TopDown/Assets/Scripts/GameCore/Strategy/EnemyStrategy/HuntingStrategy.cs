using Character;
using GameCore.StateMachine;
using UnityEngine;

namespace GameCore.Strategy
{
    class HuntingStrategy : BasicEnemyStategy
    {
        private readonly Transform _player;
        private Vector3 _randomCirclePoint;
        
        public HuntingStrategy(AbstractCharacter owner, string status) : base(owner, status)
        {
            _player = GameController.Instance.playerGO.transform;
        }

        public override void DoStrategy()
        {
            base.DoStrategy();
            if (statusController.isCombat) return;
            SetPlayerPosition();
            stateMachine.Work();
        }

        private void SetPlayerPosition()
        {
            navigationController.SetCurrentPoint(_player.position + _randomCirclePoint);
            if (statusController.isRetreat) statusController.isRetreat = false; 
            statusController.isHunting = true;
            stateMachine.ChangeState(typeof(Move));
        }
    }
}
