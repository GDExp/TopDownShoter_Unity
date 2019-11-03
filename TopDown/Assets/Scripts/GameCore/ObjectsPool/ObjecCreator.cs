using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    class ObjecCreator : ICreator
    {
        //to do настройка через псевдо-конфиг
        private Dictionary<Type, GameObject> _objectLibrary;

        public ObjecCreator()
        {
            _objectLibrary = new Dictionary<Type, GameObject>();

            //to do  - remove late?
            var psevdoConfig = Resources.Load<PrefabsLibrary>("PrefabsLibrary");
            psevdoConfig.GetConfigLibrary(_objectLibrary);
        }

        public IPoolableObject CreatePoolObject(Type objectType)
        {
            GameObject currentObject = null;
            if (CheckExeptionInLibrary(objectType)) currentObject = UnityEngine.Object.Instantiate(_objectLibrary[objectType]);

            return currentObject.GetComponent<IPoolableObject>();
        }

        private bool CheckExeptionInLibrary(Type type)
        {
            bool result = true;
            try
            {
                if (!_objectLibrary.ContainsKey(type)) throw new Exception();
            }
            catch(Exception e)
            {
                CustomDebug.LogMessage(e, DebugColor.red);
                CustomDebug.LogMessage($"No object - {type} in CreatorLibrary!", DebugColor.red);
                result = false;
            }

            return result;
        }
    }
}
