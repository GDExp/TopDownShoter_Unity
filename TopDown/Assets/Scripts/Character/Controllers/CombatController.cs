using System;
using UnityEngine;
using GameCore;
using GameCore.StateMachine;
using Observer;


namespace Character
{
    interface ICombatController
    {
        void Attack();
    }

    class CombatController<T> : ICombatController, IReceiver<AnimationValue<AbstractCharacter>>, IObserver
        where T : AbstractCharacter
    {
        private readonly T _owner;
        private IStateMachine _stateMachine;
        private GameObject testHit;//it remove
        private bool isAttacked;

        public CombatController(T owner, GameObject ownerGO)
        {
            _owner = owner;
            testHit = ownerGO.GetComponentInChildren<HitColliderLink>().gameObject;
            testHit.SetActive(!testHit.activeSelf);
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
                Attack();
            }
            else
            {
                _owner.Unsubscribe(typeof(AnimationEventCallback), this);
                testHit.SetActive(!testHit.activeSelf);
                ToggleIsCombat();
                if (_stateMachine is null) _stateMachine = _owner.GetStateMachine() as IStateMachine;
                _stateMachine.ChangeState(typeof(Idle));
            }
        }

        public void Attack()
        {
            //to do переделать в каст коллизий с дальнейшей обработкой урона
            testHit.SetActive(!testHit.activeSelf);
            isAttacked = !isAttacked;
        }

        private void ToggleIsCombat()
        {
            var status = _owner.GetStatusController() as StatusController;
            status.isCombat = isAttacked;
        }
    }
}
