using System;
using Character;

namespace GameCore
{
    class AttackAnimationCommand : BaseAnimationCommand<AnimationValue<AbstractCharacter>>
    {
        private readonly IReceiver<AnimationValue<AbstractCharacter>> _combatReceiver;

        public AttackAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0, bool attackStatus = false, bool rangeStatus = false) 
            : base(invoker)
        {
            if (invoker == null) return;
            argValue = new AnimationValue<AbstractCharacter>(invoker, animationType, animationValue, attack: attackStatus, range: rangeStatus);

            receivers.Add(invoker.AnimationController as IReceiver<AnimationValue<AbstractCharacter>>);
            receivers.Add(invoker.CombatController as IReceiver<AnimationValue<AbstractCharacter>>);
        }
    }
}
