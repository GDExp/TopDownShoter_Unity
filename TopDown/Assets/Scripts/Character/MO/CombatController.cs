using System;
using UnityEngine;
using GameCore;
using GameCore.Strategy;
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
        private GameObject testHit;
        private int _currnetAttackStep;
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
            ToggleCurrentStrategyInCombat();
            isAttacked = true;
        }

        public void Attack()
        {
            //to do переделать в каст коллизий с дальнейшей обработкой урона
            testHit.SetActive(!testHit.activeSelf);
            isAttacked = !isAttacked;
        }

        private void ToggleCurrentStrategyInCombat()
        {
            var currentStrategy = _owner.GetCharacterStrategy() as BaseStrategy;
            currentStrategy?.CombatTransition();
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
                ToggleCurrentStrategyInCombat();
            }            
        }
    }
}
