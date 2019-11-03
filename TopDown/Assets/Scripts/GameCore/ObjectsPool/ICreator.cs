using System;

namespace GameCore
{
    interface ICreator
    {
        IPoolableObject CreatePoolObject(Type objectType);
    }
}
