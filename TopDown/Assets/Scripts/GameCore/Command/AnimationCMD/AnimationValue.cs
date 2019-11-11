﻿using System;

namespace GameCore
{
    readonly struct AnimationValue<T>
    {
        public readonly T owner;
        public readonly Type animationType;
        public readonly float animationValue;
        public readonly bool isAttack;
        public readonly bool isDead;

        public AnimationValue(T owner, Type type, float value,  bool attack = false, bool dead = false)
        {
            this.owner = owner;
            animationType = type;
            animationValue = value;
            isAttack = attack;
            isDead = dead;
        }
    }
}
