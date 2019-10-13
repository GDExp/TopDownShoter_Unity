using UnityEngine;

namespace GameCore
{
    class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public GameObject playerGO { get; private set; }

        public float xValue;
        public float zValue;

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
        }

        private void Update()
        {
            xValue = Input.GetAxisRaw("Horizontal");
            zValue = Input.GetAxisRaw("Vertical");
        }

        public void SetPlayerReference(GameObject player)
        {
            playerGO = player;
        }
    }
}
