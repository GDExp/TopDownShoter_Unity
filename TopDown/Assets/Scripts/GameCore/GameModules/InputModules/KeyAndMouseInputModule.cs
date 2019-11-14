using Character;
using UnityEngine;

namespace GameCore
{
    //используем мышь для навигации и вертикальную ось
    //эмуляция джойстика
    class KeyAndMouseInputModule : PlayerInputModule
    {
        public KeyAndMouseInputModule(PlayerCharacter owner) : base(owner)
        {
            isJoystick = true;
        }

        protected override void LookAt()
        {
            if(owner.StatusController.isCombat) return;
            base.LookAt();
        }

        protected override void SetMoveValue()
        {
            isMove = Input.GetAxisRaw("Vertical") > 0 && !isAttack;
            movePoint = (isMove) ? player.forward : player.position;
        }

        protected override void SetAttackValue()
        {
            if(Input.GetMouseButtonDown(0)) owner.CombatController.SetAttackType(AttackType.Melee);
            if (Input.GetMouseButtonDown(1)) owner.CombatController.SetAttackType(AttackType.Range);
            isAttack = (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ) & owner.StatusController.CheckReloadTime(Time.time);
        }
    }
}
