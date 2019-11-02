using System;

namespace GameCore
{
    interface IStorage
    {
        bool CheckObjectInStorage(Type objectType);
        object GetObjectInStorage(Type objectType);
        void SetObjectInStorage(object currentObject);
    }
}
