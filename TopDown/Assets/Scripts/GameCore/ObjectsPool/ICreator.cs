using System;

namespace GameCore
{
    interface ICreator
    {
        object CreatePoolObject(Type objectType);
    }
}
