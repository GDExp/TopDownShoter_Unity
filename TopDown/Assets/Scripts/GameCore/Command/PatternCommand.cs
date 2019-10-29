using System;

namespace GameCore
{
    class PatternCommand<T> : ICommand
        where T: class
    {
        protected readonly T _invoker;
        protected IReceiver<AnimationValue<T>> _receiver;
        protected readonly Type _animationType;
        protected readonly float _animationValue;

        public PatternCommand(T invoker, Type animationType, float animationValue = 0f)
        {
            _invoker = invoker;
            _animationType = animationType;
            _animationValue = animationValue;
        }

        public virtual void Execute()
        {

        }
    }
}
