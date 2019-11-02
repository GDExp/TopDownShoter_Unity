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
        [HideInInspector] public Vector3 startPosition { get; private set; }
        public AbstractCharacter currentTarget { get; private set; }
        public Transform targetTransform { get; private set; }
        private Dictionary<TypeConduct, IStrategy> enemyStrategy;

        private float distanceToPlayer;
        public float visionRadius;// to do setup in editor - prefab
        [Range(0f,100f)]
        public float brainWeight;// to do setup in editor - prafab
        public float attackDistance;// to do setup in editor - prefab
        public float targetSpeed { get; private set; }
        public int healingPower;// to do setup in editor - prefab

        public bool isSmart { get; private set; }
        private bool isRetreatOnce;

        protected override void SetupCharacter()
        {
            isPlayMode = true;
            base.SetupCharacter();
            strSwither = new StrategySwithcer();
            startPosition = transform.localPosition;
            isSmart = brainWeight >= Random.Range(45f, 75f);

            currentTarget = GameCore.GameController.Instance.playerGO.GetComponent<PlayerCharacter>();// test
            targetTransform = GameCore.GameController.Instance.playerGO.transform;//test
            navigationController.SetAgentSpeed(statusController.maxSpeed * 0.6f);//test

            attackDistance = navigationController.GetAgentStopDistance();//only if malee attack type;

            SetupStrategy();
            StartCoroutine(CheckTargetDistance());
        }

        private void SetupStrategy()
        {
            enemyStrategy = new Dictionary<TypeConduct, IStrategy>
            {
                { TypeConduct.Idle, new IdleStrategy(this, TypeConduct.Idle.ToString()) },
                { TypeConduct.Hunting, new HuntingStrategy(this, TypeConduct.Hunting.ToString()) },
                { TypeConduct.Return, new ReturnStrategy(this, TypeConduct.Return.ToString()) },
                { TypeConduct.Attack, new AttackStrategy(this, TypeConduct.Attack.ToString()) },
                { TypeConduct.Retreat, new RetreatStrategy(this, TypeConduct.Retreat.ToString()) },
            };
        }

        private IEnumerator CheckTargetDistance()
        {
            Vector3 lastPosition;
            while (gameObject.activeInHierarchy)
            {
                distanceToPlayer = (transform.position - targetTransform.position).magnitude;
                if (statusController.isHunting)
                {
                    lastPosition = targetTransform.position;
                    yield return new WaitForEndOfFrame();
                    targetSpeed = (targetTransform.position - lastPosition).magnitude / Time.deltaTime;
                    targetSpeed = Mathf.RoundToInt(targetSpeed);
                }
                yield return new WaitForFixedUpdate();
            }
        }

        public override void UpdateCharacter()
        {
            base.UpdateCharacter();
            if (distanceToPlayer <= visionRadius && !statusController.isRetreat)
            {
                if (distanceToPlayer <= attackDistance) strSwither.SetStrategy(enemyStrategy[TypeConduct.Attack]);
                else strSwither.SetStrategy(enemyStrategy[TypeConduct.Hunting]);
            }
            else
            {
                if (!statusController.isRetreat) strSwither.SetStrategy(enemyStrategy[TypeConduct.Idle]);
                else strSwither.SetStrategy(enemyStrategy[TypeConduct.Retreat]);
                if (statusController.isHunting) strSwither.SetStrategy(enemyStrategy[TypeConduct.Return]);
            }

            if (statusController.CheckCurrentHealthToLimit(HealthStatus.LowHealth)
                && !isRetreatOnce
                && isSmart)
            {
                strSwither.SetStrategy(enemyStrategy[TypeConduct.Retreat]);
                isRetreatOnce = true;
            }

            strSwither.StrategyIsWork();
        }

        private bool ChangeStatusByDistance()
        {
            distanceToPlayer = (transform.position - targetTransform.position).magnitude;
            return distanceToPlayer < visionRadius;
        }

#if UNITY_EDITOR
        // to do - вынос в отдельный класс
        // only editor visual
        public string enemyStatus;//visal strategy
        private bool isPlayMode;
        private void OnDrawGizmosSelected()
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
