using System;
using Character;

namespace GameCore
{
    class DeadAnimationCommand : BaseAnimationCommand<AnimationValue<AbstractCharacter>>
    {

        public DeadAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0) : base(invoker)
        {
            argValue = new AnimationValue<AbstractCharacter>(invoker, animationType, animationValue);
            receivers.Add(invoker.animationController as IReceiver<AnimationValue<AbstractCharacter>>);
        }
    }
}
