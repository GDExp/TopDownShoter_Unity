using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class PlayerMovementLogic : AbstractMovementLogic<AbstractCharacter>
    {
        private readonly PlayerCharacter _player;
        private readonly Transform _playerTransform;

        public PlayerMovementLogic(AbstractCharacter owner) : base(owner)
        {
            _player = owner as PlayerCharacter;
            _playerTransform = owner.transform;
#if KEY_AND_MOUSE
            _player.playerInput = new KeyAndMouseInputModule(_player);
#endif
#if ONLY_MOUSE
            _player.playerInput = new OnlyMouseInputModule(_player);
#endif
            GameController.Instance.AddModuleInList(_player.playerInput);
        }

        protected override void InputCurrentPoint()
        {
            Vector3 point = Vector3.zero;

            if (_player.playerInput.isMove)
            {
                if (isStoped) isStoped = !navigationController.InteractObstacleComponent();
                if (_player.playerInput.isJoystick)
                {
                    point = _playerTransform.position + _player.playerInput.movePoint * navigationController.GetAgentStopDistance();
                }
                else
                {
                    point = _player.playerInput.movePoint;
                }
            }
            navigationController?.SetCurrentPoint(point);
        }
    }
}
