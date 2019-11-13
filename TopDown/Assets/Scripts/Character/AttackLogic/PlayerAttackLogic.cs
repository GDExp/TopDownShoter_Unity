using UnityEngine;
using GameCore;

namespace Character
{
    class PlayerAttackLogic : BaseAttackLogic<AbstractCharacter>
    {
        private readonly PlayerCharacter player;

        private float _attackRadius;

        public PlayerAttackLogic(AbstractCharacter owner) : base(owner)
        {
            player = owner as PlayerCharacter;
            _attackRadius = 2f;
        }

        //TO DO это нужно менять на что-то поинтереснее
        protected override void MeleeAttack()
        {
            base.MeleeAttack();
            var attackResult = Physics.SphereCastAll(combatController.attackPoint.position, _attackRadius, combatController.attackPoint.forward, 1f);
            var result = Physics.OverlapSphere(combatController.attackPoint.position, _attackRadius);

            for (int i = 0; i < attackResult.Length; ++i)
            {
                if (attackResult[i].collider.CompareTag("Enemy"))
                {
                    var enemy = attackResult[i].collider.GetComponent<EnemyCharacter>();
                    ICommand attackCMD = new AttackDamageCommand(enemy, player, 25);//25 - test
                    attackCMD.Execute();
                }
            }
        }
    }
}
