using System;
using Character;

namespace GameCore
{
    class BaseAnimationCommand : PatternCommand<AbstractCharacter>
    {
        protected IReceiver<AnimationValue<AbstractCharacter>> _receiver;
        protected readonly Type _animationType;
        protected readonly float _animationValue;

        public BaseAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0) : base(invoker)
        {
            _animationType = animationType;
            _animationValue = animationValue;
        }
    }
}
