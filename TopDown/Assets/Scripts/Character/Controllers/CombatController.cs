using System;
using UnityEngine;
using GameCore;
using GameCore.StateMachine;
using Observer;


namespace Character
{
    public enum AttackType
    {
        Melee,
        Range,
    }

    public class CombatController<T> : IReceiver<AnimationValue<AbstractCharacter>>, IObserver
        where T : AbstractCharacter
    {
        public readonly Transform attackPoint;
        public BaseProjectile currentProjectile;//test
        public float rangePower;

        private readonly T _owner;
        private readonly Transform _transform;

        private AttackType _currentAttackType;
        public AttackType CurrentAttackType { get => _currentAttackType; }

        private IAttackLogic _currentAttackLogic;
        private Vector3 _attackPointOffset;
        private bool isAttacked;

        public CombatController(T owner)
        {
            _owner = owner;
            _transform = owner.transform;
            var characterComponent = owner.GetComponent<CharacterController>();

            //test
            var attackPoint = new GameObject("AttackPoint");
            this.attackPoint = attackPoint.transform;
            this.attackPoint.SetParent(_transform);
            this.attackPoint.position = _transform.position + new Vector3(0f, characterComponent.height/2f, characterComponent.radius * 3f);
            _currentAttackType = owner.currentAttackType;//test
            rangePower = 75f;//test
        }

        public void SetAttackLogic()
        {
            if (_owner is PlayerCharacter) _currentAttackLogic = new PlayerAttackLogic(_owner);
            else _currentAttackLogic = new EnemyAttackLogic(_owner);
        }

        public void SetAttackType(AttackType type)
        {
            _currentAttackType = type;
        }

        public void HandleCommand(AnimationValue<AbstractCharacter> value)
        {
            _owner.Subscribe(typeof(AnimationEventCallback), this);
            isAttacked = true;
            ToggleIsCombat();
        }

        public void UpdateObserver(Type subjectTypeCallback)
        {
            if (isAttacked)
            {
                AttackEvent();
            }
            else
            {
                _owner.Unsubscribe(typeof(AnimationEventCallback), this);
                ToggleIsCombat();
                _owner.StateMachine.ChangeState(typeof(Idle));
            }
        }

        public void AttackEvent()
        {
            isAttacked = !isAttacked;
            _currentAttackLogic.Attack();
        }

        private void ToggleIsCombat()
        {
            _owner.StatusController.isCombat = isAttacked;
        }
    }
}
