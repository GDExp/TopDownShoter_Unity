using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using Observer;

namespace Character
{

    class AnimationController : IReceiver<AnimationValue<AbstractCharacter>>, IObserver
    {
        private const string AttackTrigger = "Attack";
        private const string DeadTrigger = "Dead";

        private readonly Dictionary<Type, string> _animatonKeys;
        private Animator _animator;
        private Type _currentTypeAnimation;
        private bool _isAttackEvent;
        
        public AnimationController(Animator animator, Dictionary<Type,string> animationKeys)
        {
            _animatonKeys = animationKeys;
            _animator = animator;
        }

        //IRecevier
        public void HandleCommand(AnimationValue<AbstractCharacter> inputValue)
        {
            _animator.SetFloat(_animatonKeys[inputValue.animationType], inputValue.animationValue);
            _currentTypeAnimation = inputValue.animationType;
            if (inputValue.isAttack)
            {
                _animator.SetTrigger(AttackTrigger);//???
                _isAttackEvent = true;
            }
            if (inputValue.isDead) _animator.SetTrigger(DeadTrigger);
        }

        public void UpdateObserver(Type subjectTypeCallback)
        {
            ResetCurrentState();
        }

        private void ResetCurrentState()
        {
            if (_isAttackEvent)
            {
                _isAttackEvent = false;
                return;
            }
            _animator.SetFloat(_animatonKeys[_currentTypeAnimation], 0f);
        }
    }
}
