using UnityEngine;

namespace Character
{
    class BaseAttackLogic<T> : IAttackLogic
        where T: AbstractCharacter
    {
        protected T owner;
        protected readonly CombatController<T> combatController;

        public BaseAttackLogic(T owner)
        {
            this.owner = owner;
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
            var gameController = GameCore.GameController.Instance;
            var type = gameController.projectileConvert.GetConvertProjectileType(owner.currentProjectileType);
            var go = gameController.objectsPool.ProvideObject(type) as IProjectile;
            go.SetupProjectile(owner);
            go.SetStartPosition(combatController.attackPoint);
            go.AddProjectileForce(combatController.rangePower);
        }
    }
}
