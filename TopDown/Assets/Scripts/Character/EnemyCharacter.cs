using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
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
        [Range(0f, 200f)]
        public float patrolDistance;//to do setup in editor
        [Range(0f,100f)]
        public float brainWeight;// to do setup in editor - prafab
        [Range(0f, 100f)]
        public float attackDistance;// to do setup in editor - prefab
        public float targetSpeed { get; private set; }
        public int healingPower;// to do setup in editor - prefab

        public bool isDead { get; private set; }
        public bool isSmart { get; private set; }
        private bool isRetreatOnce;

        protected override void SetupCharacter()
        {
            tag = "Enemy";
            isPlayMode = true;

            base.SetupCharacter();

            strSwither = new StrategySwithcer();
            startPosition = transform.localPosition;
            isSmart = brainWeight >= Random.Range(45f, 75f);

            ICommand speedCMD = new ChangeAnimationSpeedCommand(this, SpeedStatus.NormalSpeed);
            speedCMD.Execute();

            currentTarget = GameController.Instance.characterModule.player;// test
            targetTransform = currentTarget.transform;//test

            if (currentAttackType == AttackType.Melee) attackDistance = navigationController.GetAgentStopDistance();//only if malee attack type;
            else animationController.SetRangeAnimation();

            SetupStrategy();
            StartCoroutine(CheckTargetDistance());
        }

        //TO DO Add unquie AI logic in game core
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
            if (isDead) return;
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

        protected override void CharacterDead()
        {
            isDead = true;
            base.CharacterDead();
        }

#if UNITY_EDITOR
        // to do - вынос в отдельный класс
        // only editor visual
        public string enemyStatus;//visal strategy
        [Range(0f,100f)]
        public float labelDistance;
        private bool isPlayMode;

        private void OnDrawGizmosSelected()
        {
            //character visual
            Gizmos.color = Color.red;
            var drawPosition = (isPlayMode) ? startPosition : transform.position;
            Gizmos.DrawSphere(drawPosition, 1f);
            Gizmos.DrawWireSphere(transform.position, visionRadius);
            
            //for patrol distance
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(drawPosition, patrolDistance);
        }

        private void OnDrawGizmos()
        {
            int visualHP = (statusController is null) ? 0 : statusController.CurrentHealth;

            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            var positionLabel = transform.position + Vector3.up * labelDistance;
            float step = 5f;
            UnityEditor.Handles.Label(positionLabel + Vector3.up * step, $"<color=yellow>{enemyStatus}</color>", style);
            UnityEditor.Handles.Label(positionLabel + Vector3.up * 2 * step, $"<color=yellow>{visualHP}</color>", style);
        }
#endif
    }
}
