using System;

namespace GameCore
{
    class GameObjectPool
    {
        private readonly IStorage _storage;
        private readonly ICreator _creator;

        public GameObjectPool()
        {
            _storage = new PoolStorage();
            _creator = new ObjecCreator();
        }

        public object ProvideObject(Type objectType)
        {
            object currentobject = null;

            if (_storage.CheckObjectInStorage(objectType)) currentobject = _storage.GetObjectInStorage(objectType);
            else currentobject = _creator.CreatePoolObject(objectType);

            return currentobject;
        }

        public void ReturnObject(object currentObject)
        {
            _storage.SetObjectInStorage(currentObject);
        }
    }
}
