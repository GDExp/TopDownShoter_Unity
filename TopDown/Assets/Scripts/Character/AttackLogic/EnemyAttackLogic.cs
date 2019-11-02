using UnityEngine;
using GameCore;

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
            base.MeleeAttack();
            float hitDistance = (enemy.transform.position - enemy.targetTransform.position).magnitude;
            if (hitDistance <= enemy.attackDistance)
            {
                var attackCMD = new AttackDamageCommand(enemy.currentTarget, enemy, 25);// 25 - test
                attackCMD.Execute();
            }
        }
    }
}
