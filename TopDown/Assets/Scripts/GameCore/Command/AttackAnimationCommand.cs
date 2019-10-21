using System;
using Character;

namespace GameCore
{
    class AttackAnimationCommand : PatternCommand<AbstractCharacter>, ICommand
    {
        private readonly bool isAttack;
        private readonly IReceiver<AnimationValue<AbstractCharacter>> _combatReceiver;

        public AttackAnimationCommand(AbstractCharacter invoker, Type animationType, float animationValue = 0, bool attackStatus = false) 
            : base(invoker, animationType, animationValue)
        {
            _receiver = invoker.GetAnimationController() as IReceiver<AnimationValue<AbstractCharacter>>;
            _combatReceiver = invoker.GetCombatController() as IReceiver<AnimationValue<AbstractCharacter>>;
            isAttack = attackStatus;
        }

        public void Execute()
        {
            var argValue = 
                new AnimationValue<AbstractCharacter>(_invoker, _animationType, _animationValue, attack: isAttack);
            _receiver?.HandleCommand(argValue);
            _combatReceiver?.HandleCommand(argValue);
        }
    }
}
