using UnityEngine;

namespace Character
{
    class EnemyAttackLogic : BaseAttackLogic<AbstractCharacter>
    {
        private readonly EnemyCharacter enemy;

        public EnemyAttackLogic(AbstractCharacter owner) : base(owner)
        {
            enemy = owner as EnemyCharacter;
        }

        protected override void MeleeAttack()
        {
            if (combatController.currentAttackType == AttackType.Range) return;
            float hitDistance = (enemy.transform.position - enemy.targetTransform.position).magnitude;
            if (hitDistance <= enemy.attackDistance) Debug.Log($"Hiting - {enemy.targetTransform}");
        }

        protected override void RangeAttack()
        {
            //to do - do it...
        }
    }
}
