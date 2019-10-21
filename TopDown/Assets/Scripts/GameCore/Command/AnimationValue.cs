using System;

namespace GameCore
{
    struct AnimationValue<T>
    {
        public readonly T owner;
        public readonly Type animationType;
        public readonly float animationValue;
        public readonly bool isAttack;

        public AnimationValue(T owner, Type type, float value,  bool attack = false)
        {
            this.owner = owner;
            animationType = type;
            animationValue = value;
            isAttack = attack;
        }
    }
}
