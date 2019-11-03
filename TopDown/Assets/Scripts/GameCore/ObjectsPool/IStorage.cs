using System;

namespace GameCore
{
    interface IStorage
    {
        bool CheckObjectInStorage(Type objectType);
        IPoolableObject GetObjectInStorage(Type objectType);
        void SetObjectInStorage(IPoolableObject currentObject);
    }
}
