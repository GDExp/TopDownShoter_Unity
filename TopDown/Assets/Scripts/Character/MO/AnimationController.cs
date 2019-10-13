using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using Observer;

namespace Character
{

    class AnimationController : IReceiver<AnimationValue<AbstractCharacter>>, IObserver
    {
        private readonly Dictionary<Type, string> _animatonKeys;
        private Animator _animator;
        private Type _currentTypeAnimation;
        private CharacterStateType _currentStateType;


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
            _currentStateType = inputValue.stateType;
            if (inputValue.isAttack) _animator.SetTrigger(inputValue.stateType.ToString());
        }

        public void UpdateObserver(Type subjectTypeCallback)
        {
            ProcessAnimationEvent();
        }

        private void ProcessAnimationEvent()
        {
            //Расширить!
            switch (_currentStateType)
            {
                case (CharacterStateType.Attack):
                    //AttackAnimationEvent();
                    break;
                default:
                    ResetCurrentState();
                    break;
            }
        }
        
        private void AttackAnimationEvent()
        {
            _currentStateType = CharacterStateType.None;
        }

        private void ResetCurrentState()
        {
            _animator.SetFloat(_animatonKeys[_currentTypeAnimation], 0f);
        }
    }
}
