using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    class PoolStorage : IStorage
    {
        private readonly Transform _storageTransform;
        private Dictionary<Type, List<IPoolableObject>> _objectStorage;

        public PoolStorage()
        {
            _storageTransform = new GameObject("PoolStorage").transform;
            _storageTransform.position = Vector3.one * 9999f;
            _objectStorage = new Dictionary<Type, List<IPoolableObject>>();
        }

        public bool CheckObjectInStorage(Type objectType)
        {
            bool resut = _objectStorage.ContainsKey(objectType);
            if (resut) resut = _objectStorage[objectType].Count > 0;
            return resut;
        }

        public IPoolableObject GetObjectInStorage(Type objectType)
        {
            var list = _objectStorage[objectType];
            var currentObj = list[0];
            _objectStorage[objectType].Remove(currentObj);
            currentObj.EnablePoolObject();
            return currentObj;
        }

        public void SetObjectInStorage(IPoolableObject currentObject)
        {
            var go = currentObject as IPoolableObject;
            var type = go.GetObjectType();
            if (!_objectStorage.ContainsKey(type)) _objectStorage.Add(type, new List<IPoolableObject>());
            _objectStorage[type].Add(currentObject);
            go.DisablePoolObject(_storageTransform);
        }
    }
}
