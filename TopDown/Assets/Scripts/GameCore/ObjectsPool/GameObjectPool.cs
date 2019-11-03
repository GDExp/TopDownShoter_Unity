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

        public IPoolableObject ProvideObject(Type objectType)
        {
            IPoolableObject currentobject = null;

            if (_storage.CheckObjectInStorage(objectType)) currentobject = _storage.GetObjectInStorage(objectType);
            else currentobject = _creator.CreatePoolObject(objectType);

            return currentobject;
        }

        public void ReturnObject(IPoolableObject currentObject)
        {
            _storage.SetObjectInStorage(currentObject);
        }
    }
}
