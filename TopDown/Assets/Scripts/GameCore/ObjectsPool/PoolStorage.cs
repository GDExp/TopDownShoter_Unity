using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    class PoolStorage : IStorage
    {
        private readonly Transform _storageTransform;
        private Dictionary<Type, List<object>> _objectStorage;

        public PoolStorage()
        {
            _storageTransform = new GameObject("PoolStorage").transform;
            _storageTransform.position = Vector3.one * 9999f;
            _objectStorage = new Dictionary<Type, List<object>>();
        }

        public bool CheckObjectInStorage(Type objectType)
        {
            bool resut = _objectStorage.ContainsKey(objectType);
            if (resut) resut = _objectStorage[objectType].Count > 0;
            return resut;
        }

        public object GetObjectInStorage(Type objectType)
        {
            var list = _objectStorage[objectType];
            object currentObject = list[0];
            _objectStorage[objectType].Remove(currentObject);
            TakeObjectInPool(currentObject as GameObject);
            return currentObject;
        }

        private void TakeObjectInPool(GameObject poolObject)
        {
            poolObject.SetActive(true);
            poolObject.transform.SetParent(null);
        }

        public void SetObjectInStorage(object currentObject)
        {
            var go = currentObject as GameObject;
            var type = go.GetComponent<IPoolableObject>().GetObjectType();
            if (!_objectStorage.ContainsKey(type)) _objectStorage.Add(type, new List<object>());
            _objectStorage[type].Add(currentObject);
            DropObjectInStorage(currentObject as GameObject);
        }

        private void DropObjectInStorage(GameObject poolObject)
        {
            poolObject.transform.SetParent(_storageTransform);
            poolObject.transform.position = Vector3.zero;
            poolObject.SetActive(false);
        }
    }
}
