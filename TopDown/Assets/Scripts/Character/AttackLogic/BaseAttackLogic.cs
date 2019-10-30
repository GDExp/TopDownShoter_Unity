
namespace Character
{
    abstract class BaseAttackLogic<T> : IAttackLogic
        where T: AbstractCharacter
    {
        protected readonly CombatController<T> combatController;

        public BaseAttackLogic(T owner)
        {
            combatController = owner.GetCombatController() as CombatController<T>;
        }

        public void Attack()
        {
            this.MeleeAttack();
            this.RangeAttack();
        }

        protected abstract void MeleeAttack();
        protected abstract void RangeAttack();
    }
}
