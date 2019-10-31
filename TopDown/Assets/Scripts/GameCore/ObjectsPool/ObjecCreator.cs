using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    class ObjecCreator : ICreator
    {
        //to do настройка через псевдо-конфиг
        private Dictionary<Type, object> _objectLibrary;

        public ObjecCreator()
        {
            _objectLibrary = new Dictionary<Type, object>();
            //test
            var psevdoConfig = Resources.Load<PrefabsLibrary>("PrefabsLibrary");
            psevdoConfig.GetConfigLibrary(_objectLibrary);
        }

        public object CreatePoolObject(Type objectType)
        {
            object currentObject = null;
            Debug.Log("Create!");
            if (CheckExeptionInLibrary(objectType)) currentObject = UnityEngine.Object.Instantiate(_objectLibrary[objectType] as GameObject);

            return currentObject;
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
