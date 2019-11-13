using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using Observer;

namespace Character
{

    public class AnimationController : IReceiver<AnimationValue<AbstractCharacter>>, IReceiver<AnimationSpeedValue<AbstractCharacter>>, IObserver
    {
        private const string AnimationSpeedMultiplier = "SpeedMultiplier";
        private const string AttackTrigger = "Attack";
        private const string RangeStatus = "Is Range";
        private const string DeadTrigger = "Dead";

        private readonly Dictionary<Type, string> _animatonKeys;
        private Animator _animator;
        private Type _currentTypeAnimation;
        private bool _isAttackEvent;
        private bool _isRangeKey;
        
        public AnimationController(Animator animator, Dictionary<Type,string> animationKeys)
        {
            _animatonKeys = animationKeys;
            _animator = animator;
        }

        public void SetRangeAnimation()
        {
            _isRangeKey = true;
            _animator.SetBool(RangeStatus, _isRangeKey);
        }

        //IRecevier - animation
        public void HandleCommand(AnimationValue<AbstractCharacter> inputValue)
        {
            _animator.SetFloat(_animatonKeys[inputValue.animationType], inputValue.animationValue);
            _currentTypeAnimation = inputValue.animationType;
            if (inputValue.isAttack)
            {
                _animator.SetBool(RangeStatus, inputValue.isRange);
                _animator.SetTrigger(AttackTrigger);
                _isAttackEvent = true;
            }
            if (inputValue.isDead) _animator.SetTrigger(DeadTrigger);
        }

        //IReceiver - animation speed
        public void HandleCommand(AnimationSpeedValue<AbstractCharacter> inputValue)
        {
            _animator.SetFloat(AnimationSpeedMultiplier, inputValue.multiplier);
        }

        public void UpdateObserver(Type subjectTypeCallback)
        {
            ResetCurrentState();
        }

        private void ResetCurrentState()
        {
            if (_isAttackEvent)
            {
                _animator.SetBool(RangeStatus, _isRangeKey);
                _isAttackEvent = false;
                return;
            }
            _animator.SetFloat(_animatonKeys[_currentTypeAnimation], 0f);
        }
    }
}
