using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameCore
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        public GameObjectPool objectsPool { get; private set; }
        public ProjectileConvert projectileConvert;//test
        
        public CharactersModule characterModule { get; private set; }
        public ProjectilesModule projectileModule { get; private set; }
        
        private List<IUpdatableModule> _allUpdatableModule;

        private bool isGamePlay;

        [RuntimeInitializeOnLoadMethod]
        public static void InitializedGameController()
        {
            CreatingGameController();
        }

        private static void CreatingGameController()
        {
            GameObject gameControllerGO = new GameObject("GameController");
            gameControllerGO.AddComponent(typeof(GameController));
            DontDestroyOnLoad(gameControllerGO);
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            objectsPool = new GameObjectPool();
            projectileConvert = new ProjectileConvert();

            _allUpdatableModule = new List<IUpdatableModule>();
            characterModule = new CharactersModule(this);
            projectileModule = new ProjectilesModule(this);
            

            isGamePlay = true;
        }

        private void OnDisable()
        {
            isGamePlay = false;
        }
        private void OnDestroy()
        {
            isGamePlay = false;
        }

        private void Start()
        {
            StartCoroutine(OnUpdateModules());
        }

        private IEnumerator OnUpdateModules()
        {
            for (int i = 0; i < _allUpdatableModule.Count; ++i) _allUpdatableModule[i].OnStart();

            while (isGamePlay)
            {
                for (int i = 0; i < _allUpdatableModule.Count; ++i) _allUpdatableModule[i].OnUpdate();
                yield return new WaitForFixedUpdate();
            }
        }

        public void AddModuleInList(IUpdatableModule module)
        {
            CustomDebug.LogMessage($"Add module - {module}", DebugColor.orange);
            if (_allUpdatableModule.Contains(module)) return;
            _allUpdatableModule.Add(module);
        }
    }
}
