using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.StateMachine;
using GameCore.Factory;
using GameCore.Strategy;
using Observer;


namespace Character
{
    public interface ISetupCharacter
    {
        void InvokeSetupCharacter();
    }

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AbstractCharacter : MonoBehaviour, ISetupCharacter, ISubject
    {
        public CharacterValueSO characterValue;

        private StatusController _statusController;
        private IStateMachine _stateMachine;
        private AnimationController _animationController;
        private NavigatonController _navigationController;
        private ICombatController _combatController;

        private StrategySwithcer strSwither;

        private Dictionary<Type, List<IObserver>> _observers;

        //test
        private void Start()
        {
            SetupCharacter();
            strSwither = new StrategySwithcer((this as PlayerCharacter)? new PlayerStrategy(_stateMachine):null);//TO DO Remove null!!
        }
        //выполнение стратегии
        //to test
        private void Update()
        {
            strSwither?.StrategyIsWork();
        }

        public object GetAnimationController()
        {
            return _animationController;
        }

        public object GetNavigationController()
        {
            return _navigationController;
        }

        public object GetCombatController()
        {
            return _combatController;
        }

        public object GetCharacterStrategy()
        {
            return strSwither.GetStrategy();
        }

        //ISetupCharacter
        public void InvokeSetupCharacter()
        {
            SetupCharacter();
        }
        
        //ISubject
        public void Subscribe(Type msgType, IObserver observer)
        {
            if (!_observers.ContainsKey(msgType)) _observers.Add(msgType, new List<IObserver>());
            if(!_observers[msgType].Contains(observer)) _observers[msgType].Add(observer);
        }
        //ISubject
        public void Unsubscribe(Type msgType, IObserver observer)
        {
            if (!_observers.ContainsKey(msgType)) return;
            if (_observers[msgType].Contains(observer)) _observers[msgType].Remove(observer);
        }
        //ISubject
        public void Notify(Type msgType)
        {
            if (!_observers.ContainsKey(msgType)) return;
            var currentList = _observers[msgType];
            int count = currentList.Count;
            for (int i = 0; i < count; ++i) currentList[i].UpdateObserver(msgType);
        }

        protected virtual void SetupCharacter()
        {
            _observers = new Dictionary<Type, List<IObserver>>();
            CheckExeptionInCharacterValue();
            SetupStatusController();
            SetupNavigationController();
            SetupCombatController();
            SetupAnimationController();
            SetupStateMachine();
        }

        private void CheckExeptionInCharacterValue()
        {
            try
            {
                if (characterValue is null)
                {
                    CustomDebug.LogMessage($"Exeption! No VALUE SO in object - <b>{gameObject.name}</b>!", DebugColor.red );
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                CustomDebug.LogMessage(e, DebugColor.red);
                var valueExeption = new CharacterValueExeption();
                characterValue = valueExeption.ChoseDefaultValue();
            }
        }

        private void SetupStateMachine()
        {
            //TO DO добавить возможность создания 
            var product = new ProductDefaultState(this);
            _stateMachine = new StateMachine<AbstractCharacter>(this, product?.CreateFunc);
        }

        private void SetupStatusController()
        {
            var statusItems = characterValue.GetStatusValue();
            _statusController = new StatusController(statusItems.Item1, statusItems.Item2);
        }

        private void SetupAnimationController()
        {
            CheckExceptionAnimatorComponent();
            var animator = SetupAnimator();
            _animationController = new AnimationController(animator, characterValue.GetAnimationKeys());
            Subscribe(typeof(AnimationEventCallback), _animationController as IObserver);
        }

        private void CheckExceptionAnimatorComponent()
        {
            try
            {
                if(GetComponent<Animator>() == null)
                {
                    CustomDebug.LogMessage($"Exeption! No comonent - ANIMATOR in object - <b>{gameObject.name}</b>!", DebugColor.red);
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                CustomDebug.LogMessage(e, DebugColor.red);
                gameObject.AddComponent<Animator>();
                CustomDebug.LogMessage($"Add new component - ANIMATOR is object - {gameObject.name}", DebugColor.blue);
            }
        }

        private Animator SetupAnimator()
        {
            var animator = GetComponent<Animator>();
            var animationValue = characterValue.GetAnimationValue();
            animator.runtimeAnimatorController = animationValue.Item1;
            animator.avatar = animationValue.Item2;

            return animator;
        }

        private void SetupNavigationController()
        {
            _navigationController = new NavigatonController(this);
        }

        private void SetupCombatController()
        {
            _combatController = new CombatController<AbstractCharacter>(this, gameObject);
            //Subscribe(typeof(AnimationEventCallback), _combatController as IObserver);
        }

        private void CallAnimationEvent()
        {
            Notify(typeof(AnimationEventCallback));
        }
    }
}