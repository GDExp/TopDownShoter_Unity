using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.StateMachine;
using GameCore.Factory;
using GameCore.Strategy;
using Observer;


namespace Character
{
    public interface ICharacter
    {
        void InvokeSetupCharacter();
        void UpdateCharacter();
    }

    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AbstractCharacter : MonoBehaviour, ICharacter, ISubject
    {
        public CharacterValueSO characterValue;

        protected NavigationController navigationController;
        protected IStateMachine stateMachine;
        protected StrategySwithcer strSwither;
        protected StatusController statusController;

        private AnimationController _animationController;
        private CombatController<AbstractCharacter> _combatController;
        
        private Dictionary<Type, List<IObserver>> _observers;

        //test visual
        public int hp_test;

        private void Start()
        {
            GameCore.GameController.Instance.AddCharacterInList(this);
        }

        private void OnDisable()
        {
            GameCore.GameController.Instance.RemoveinCharacterInList(this);
        }

        public object GetStatusController()
        {
            return statusController;
        }

        public object GetStateMachine()
        {
            return stateMachine;
        }
        
        public object GetNavigationController()
        {
            return navigationController;
        }

        public object GetAnimationController()
        {
            return _animationController;
        }

        public object GetCombatController()
        {
            return _combatController;
        }
        
        //ICharacter
        public void InvokeSetupCharacter()
        {
            SetupCharacter();
            
        }
        //ICharacter
        public virtual void UpdateCharacter()
        {
            //do....
            stateMachine.Work();
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
            //TO DO добавить возможность создания дополнительного
            var product = new ProductDefaultState(this);
            stateMachine = new StateMachine<AbstractCharacter>(this, product?.CreateFunc);
        }

        private void SetupStatusController()
        {
            statusController = new StatusController(characterValue);
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
            navigationController = new NavigationController(this);
        }

        private void SetupCombatController()
        {
            _combatController = new CombatController<AbstractCharacter>(this);
            _combatController.SetAttackLogic();
        }

        private void CallAnimationEvent()
        {
            Notify(typeof(AnimationEventCallback));
        }
    }
}