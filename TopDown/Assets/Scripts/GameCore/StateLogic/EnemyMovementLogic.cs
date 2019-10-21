using Character;


namespace GameCore.StateMachine
{
    class EnemyMovementLogic : AbstractMovementLogic<AbstractCharacter>
    {
        public EnemyMovementLogic(AbstractCharacter owner) : base(owner)
        {
        }

        protected override void InputCurrentPoint()
        {
            
        }
    }
}
