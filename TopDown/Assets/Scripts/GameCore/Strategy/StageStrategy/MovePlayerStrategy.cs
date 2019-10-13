using UnityEngine;
using Character;

namespace GameCore.Strategy
{
    class MovePlayerStrategy : IStrategy
    {
        private readonly Transform _characterTransform;
        private readonly NavigatonController _navigationController;

        public MovePlayerStrategy(AbstractCharacter character)
        {
            _characterTransform = character.transform;
            _navigationController = character.GetNavigationController() as NavigatonController;
        }

        public void DoStrategy()
        {
            FollowToPoint();
        }

        private void FollowToPoint()
        {
            float x = GameController.Instance.xValue;
            float z = GameController.Instance.zValue;

            Vector3 direction = Vector3.zero;

            if (x != 0 || z != 0) direction = new Vector3(x, 0f, z);

            _navigationController?.MakeWayPath(_characterTransform.position + direction * 4f);
        }
    }
}
