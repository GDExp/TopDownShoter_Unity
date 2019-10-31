using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;

namespace GameCore
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        public GameObject playerGO { get; private set; }
        public GameObjectPool objectPool { get; private set; }
        private List<ICharacter> _allCharacterInScene;
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

            objectPool = new GameObjectPool();

            //test - pool
            var tester = new GameObject("POOL_TESTER");
            tester.AddComponent<PoolTest>();
            //end test

            _allCharacterInScene = new List<ICharacter>();
            _allUpdatableModule = new List<IUpdatableModule>();
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
            SetupCharactersInScene();
            StartCoroutine(OnUpdateCharacters());
            StartCoroutine(OnUpdateModules());
        }

        private void SetupCharactersInScene()
        {
            foreach(var el in _allCharacterInScene)
            {
                if(el is PlayerCharacter)
                {
                    var player = el as PlayerCharacter;
                    playerGO = player.gameObject;
                    break;
                }
            }

            for (int i = 0; i < _allCharacterInScene.Count; ++i) _allCharacterInScene[i].InvokeSetupCharacter();
        }

        private IEnumerator OnUpdateCharacters()
        {
            while (isGamePlay)
            {
                for (int i = 0; i < _allCharacterInScene.Count; ++i) _allCharacterInScene[i].UpdateCharacter();
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator OnUpdateModules()
        {
            while (isGamePlay)
            {
                for (int i = 0; i < _allUpdatableModule.Count; ++i) _allUpdatableModule[i].OnUpdate();
                yield return new WaitForFixedUpdate();
            }
        }

        public void AddCharacterInList(ICharacter character)
        {
            CustomDebug.LogMessage($"Add character - {character}", DebugColor.green);
            if (_allCharacterInScene.Contains(character)) return;
            _allCharacterInScene.Add(character);
        }

        public void RemoveinCharacterInList(ICharacter character)
        {
            CustomDebug.LogMessage($"Remove character - {character}", DebugColor.red);
            if (!_allCharacterInScene.Contains(character)) return;
            _allCharacterInScene.Remove(character);
        }

        public void AddModuleInList(IUpdatableModule module)
        {
            CustomDebug.LogMessage($"Add module - {module}", DebugColor.orange);
            if (_allUpdatableModule.Contains(module)) return;
            _allUpdatableModule.Add(module);
        }
    }
}
