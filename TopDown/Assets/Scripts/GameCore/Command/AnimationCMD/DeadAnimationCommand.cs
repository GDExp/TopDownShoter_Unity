using System;
using Character;

namespace GameCore
{
    class DeadAnimationCommand : BaseAnimationCommand
    {

        public DeadAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0) : base(invoker, animationType, animationValue)
        {
            _receiver = invoker.GetAnimationController() as AnimationController;
        }

        public override void Execute()
        {
            var argValue = new AnimationValue<AbstractCharacter>(_invoker, _animationType, _animationValue, dead: true);
            _receiver?.HandleCommand(argValue);
        }
    }
}
