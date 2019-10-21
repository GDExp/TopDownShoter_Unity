using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;

namespace GameCore
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance;

        //input system modul
        public float xValue;
        public float zValue;
        public bool attackKey;

        public GameObject playerGO { get; private set; }
        private List<ICharacter> _allCharacterInScene;


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
            _allCharacterInScene = new List<ICharacter>();
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
        }

        private void Update()
        {
            xValue = Input.GetAxisRaw("Horizontal");
            zValue = Input.GetAxisRaw("Vertical");
            attackKey = Input.GetMouseButtonDown(0);
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

        public void AddInCharacterInList(ICharacter character)
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
    }
}
