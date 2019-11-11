using System;
using Character;

namespace GameCore
{
    class AttackAnimationCommand : BaseAnimationCommand<AnimationValue<AbstractCharacter>>
    {
        private readonly IReceiver<AnimationValue<AbstractCharacter>> _combatReceiver;

        public AttackAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0, bool attackStatus = false) 
            : base(invoker)
        {
            argValue = new AnimationValue<AbstractCharacter>(invoker, animationType, animationValue, attackStatus);

            receivers.Add(invoker.GetAnimationController() as IReceiver<AnimationValue<AbstractCharacter>>);
            receivers.Add(invoker.GetCombatController() as IReceiver<AnimationValue<AbstractCharacter>>);
        }
    }
}
