using System;
using Character;

namespace GameCore
{
    class ChangeAnimationCommand : BaseAnimationCommand<AnimationValue<AbstractCharacter>>
    {
        public ChangeAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0) 
            : base(invoker)
        {
            argValue = new AnimationValue<AbstractCharacter>(invoker, animationType, animationValue);
            receivers.Add(invoker?.AnimationController as IReceiver<AnimationValue<AbstractCharacter>>);
        }
    }
}
