using Character;

namespace GameCore.StateMachine
{
    abstract class AbstractMovementLogic<T>
        where T: AbstractCharacter
    {
        protected readonly NavigationController navigationController;
        protected bool isStoped;

        public AbstractMovementLogic(T owner)
        {
            navigationController = owner.GetNavigationController() as NavigationController;
            isStoped = false;
        }

        public void InteractObstacle() 
        {
            isStoped = !navigationController.InteractObstacleComponent();
        }

        public void WorkMovement()
        {
            this.InputCurrentPoint();
            this.MoveToPoint();
        }

        protected abstract void InputCurrentPoint();

        private void MoveToPoint()
        {
            if (isStoped) isStoped = !navigationController.InteractObstacleComponent();
            navigationController?.MakeWayPath();
        }
    }
}
