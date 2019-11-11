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

    class CombatController<T> : IReceiver<AnimationValue<AbstractCharacter>>, IObserver
        where T : AbstractCharacter
    {
        public readonly Transform attackPoint;
        public BaseProjectile currentProjectile;//test
        public float rangePower;

        private readonly T _owner;
        private readonly Transform _transform;

        public AttackType currentAttackType = AttackType.Melee;

        private IAttackLogic _currentAttackLogic;
        private IStateMachine _stateMachine;
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
            currentAttackType = owner.currentAttackType;//test
            rangePower = 25f;//test
        }

        public void SetAttackLogic()
        {
            if (_owner is PlayerCharacter) _currentAttackLogic = new PlayerAttackLogic(_owner);
            else _currentAttackLogic = new EnemyAttackLogic(_owner);
        }

        public void SetAttackType(AttackType type)
        {
            currentAttackType = type;
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
                if (_stateMachine is null) _stateMachine = _owner.GetStateMachine() as IStateMachine;
                _stateMachine.ChangeState(typeof(Idle));
            }
        }

        public void AttackEvent()
        {
            isAttacked = !isAttacked;
            _currentAttackLogic.Attack();
        }

        private void ToggleIsCombat()
        {
            var status = _owner.GetStatusController() as StatusController;
            status.isCombat = isAttacked;
        }
    }
}
