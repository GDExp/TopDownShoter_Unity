using UnityEngine;
using Character;

namespace GameCore.StateMachine
{
    class PlayerMovementLogic : AbstractMovementLogic<AbstractCharacter>
    {
        private Transform _characterTransform;
        public PlayerMovementLogic(AbstractCharacter owner) : base(owner)
        {
            _characterTransform = owner.transform;
        }

        protected override void InputCurrentPoint()
        {
            float x = GameController.Instance.xValue;
            float z = GameController.Instance.zValue;

            Vector3 direction = Vector3.zero;

            if (x != 0 || z != 0)
            {
                if (isStoped) isStoped = !navigationController.InteractObstacleComponent();
                direction = new Vector3(x, 0f, z);
            }
            navigationController?.SetCurrentPoint(_characterTransform.position + direction * navigationController.GetAgentStopDistance());
        }
    }
}
