using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Strategy;


namespace Character
{
    public enum TypeConduct
    {
        Idle,
        Hunting,
        Attack,
        Return,
        Retreat,
        Dead
    }

    [AddComponentMenu("Characters/Enemy")]
    class EnemyCharacter : AbstractCharacter
    {
        public string enemyStatus;//visal strategy
        public Transform playerPosition;//test
        public float visionRadius;//test
        [HideInInspector] public Vector3 startPosition;//test
        private Dictionary<TypeConduct, IStrategy> enemyStrategy;

        [SerializeField] private float distanceToPlayer;
        public float attackDistance;

        protected override void SetupCharacter()
        {
            isPlayMode = true;
            base.SetupCharacter();
            strSwither = new StrategySwithcer();
            playerPosition = GameCore.GameController.Instance.playerGO.transform;//test
            startPosition = transform.localPosition;

            SetupStrategy();
            StartCoroutine(CheckPlayerDistance());
        }

        private void SetupStrategy()
        {
            enemyStrategy = new Dictionary<TypeConduct, IStrategy>();
            enemyStrategy.Add(TypeConduct.Idle, new IdleStrategy(this, TypeConduct.Idle.ToString()));
            enemyStrategy.Add(TypeConduct.Hunting, new HuntingStrategy(this, TypeConduct.Hunting.ToString()));
            enemyStrategy.Add(TypeConduct.Return, new ReturnStrategy(this, TypeConduct.Return.ToString()));
            enemyStrategy.Add(TypeConduct.Attack, new AttackStrategy(this, TypeConduct.Attack.ToString()));
        }

        public override void UpdateCharacter()
        {
            base.UpdateCharacter();
            //контроль состояний и выбор стратегии огласно состоянию
            if (distanceToPlayer <= visionRadius)
            {
                if (distanceToPlayer <= navigationController.GetAgentStopDistance()) strSwither.SetStrategy(enemyStrategy[TypeConduct.Attack]);//to do add attack range
                else strSwither.SetStrategy(enemyStrategy[TypeConduct.Hunting]);
            }
            else
            {
                if (!statusController.isRetreat) strSwither.SetStrategy(enemyStrategy[TypeConduct.Idle]);
                if (statusController.isHunting) strSwither.SetStrategy(enemyStrategy[TypeConduct.Return]);
            }
        }

        private IEnumerator CheckPlayerDistance()
        {
            while (gameObject.activeInHierarchy)
            {
                distanceToPlayer = (transform.position - playerPosition.position).magnitude;
                yield return new WaitForFixedUpdate();
            }
        }

        private bool ChangeStatusByDistance()
        {
            distanceToPlayer = (transform.position - playerPosition.position).magnitude;
            return distanceToPlayer < visionRadius;
        }
        
#if UNITY_EDITOR
        // only editor visual
        private bool isPlayMode;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere((isPlayMode) ? startPosition : transform.position, 1f);
            Gizmos.DrawWireSphere(transform.position, visionRadius);
            GUIStyle style = new GUIStyle();
            style.fontSize = 18;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 7f, $"<color=yellow>{enemyStatus}</color>", style);
        }
#endif

    }
}
