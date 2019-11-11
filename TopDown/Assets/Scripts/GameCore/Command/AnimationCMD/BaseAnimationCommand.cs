using System;
using System.Collections.Generic;
using Character;

namespace GameCore
{
    class BaseAnimationCommand<T> : PatternCommand<AbstractCharacter>
    {
        protected T argValue;
        protected List<IReceiver<T>> receivers;

        public BaseAnimationCommand(AbstractCharacter invoker) : base(invoker)
        {
            receivers = new List<IReceiver<T>>();
        }

        public override void Execute()
        {
            for (int i = 0; i < receivers.Count; ++i) receivers[i]?.HandleCommand(argValue);
        }
    }
}
