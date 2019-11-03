using System;
using System.Collections.Generic;

namespace GameCore
{
    class BaseGameControllerModule<T> : IUpdatableModule
    {
        protected GameController gameController;
        protected readonly List<T> updatebleElements;
        protected event Action<T> elementEvent;

        public BaseGameControllerModule(GameController gameController)
        {
            this.gameController = gameController;
            this.gameController.AddModuleInList(this);
            updatebleElements = new List<T>();
        }

        public virtual void AddElementinList(T element)
        {
            if (updatebleElements.Contains(element)) return;
            updatebleElements.Add(element);
        }

        public virtual void RemoveElementInList(T elemet)
        {
            if (!updatebleElements.Contains(elemet)) return;
            updatebleElements.Remove(elemet);
        }

        public virtual void OnStart()
        {

        }

        public virtual void OnUpdate()
        {
            if (updatebleElements.Count == 0) return;
            for (int i = 0; i < updatebleElements.Count; ++i) elementEvent?.Invoke(updatebleElements[i]);
        }
    }
}