
namespace Character
{
    class BaseAttackLogic<T> : IAttackLogic
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

        protected virtual void MeleeAttack()
        {
            if (combatController.currentAttackType == AttackType.Range) return;
        }
        protected virtual void RangeAttack()
        {
            if (combatController.currentAttackType == AttackType.Melee) return;
            var projectile = combatController.CreateProjectile();
        }
    }
}
