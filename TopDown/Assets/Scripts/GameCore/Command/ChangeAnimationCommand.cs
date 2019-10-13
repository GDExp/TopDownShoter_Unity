using System;
using Character;

namespace GameCore
{
    class ChangeAnimationCommand : PatternCommand<AbstractCharacter>, ICommand
    {
        public ChangeAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0) 
            : base(invoker, animationType, animationValue)
        {
            _receiver = invoker.GetAnimationController() as IReceiver<AnimationValue<AbstractCharacter>>;
        }

        public void Execute()
        {
            _receiver?.HandleCommand(new AnimationValue<AbstractCharacter>(_invoker, _animationType, _animationValue));
        }
    }
}
