using System;
using System.Collections.Generic;
using GameCore;

namespace Character
{
    public enum SpeedStatus
    {
        NormalSpeed,
        RunSpeed,
        ExtraRunSpeed,
    }

    public enum HealthStatus
    {
        MaxHealth,
        MediumHealth,
        LowHealth,
    }

    public class StatusController : IReceiver<AnimationSpeedValue<AbstractCharacter>>,
                                    IReceiver<DamageValue<AbstractCharacter>>,
                                    IReceiver<HealingValue<AbstractCharacter>>
    {
        private const float ConstSpeedModificator = 10f;

        public SpeedStatus speedStatus { get; private set; }

        private event Action _deadAction;
        private readonly Dictionary<SpeedStatus, float> speedModificator;

        public readonly int maxHealth;
        public readonly int maxEnergy;
        public readonly float maxSpeed;

        public int currentHealth { get; private set; }
        private int _currentEnergy;

        private float _reloadValue;
        private float _reloadTime;
        private float _healingPower;

        public bool isPatrole;
        public bool isRange;
        public bool isCombat;
        public bool isHunting;
        public bool isRetreat;

        //test
        public bool isCheat { get; private set; }


        public StatusController(CharacterValueSO valueSO, Action deadAction)
        {
            _deadAction += deadAction;

            speedModificator = valueSO.GetSpeedModificator();

            maxHealth = valueSO.characterHealth;
            maxEnergy = valueSO.characterEnergy;
            maxSpeed = valueSO.characterSpeed;

            currentHealth = maxHealth;
            _currentEnergy = maxEnergy;

            _reloadValue = valueSO.characterReload;
            
            //test
            isCheat = valueSO.isCheat;
        }

        public void HandleCommand(AnimationSpeedValue<AbstractCharacter> value)
        {
            speedStatus = value.speedStatus;
        }

        public void HandleCommand(DamageValue<AbstractCharacter> value)
        {
            if (isCheat) return;
            if (isRetreat) isRetreat = false;
            var deltaDamage = currentHealth - value.damageValue;
            currentHealth = (deltaDamage > 0) ? deltaDamage : Dead();
            CustomDebug.LogMessage(currentHealth);
        }

        public void HandleCommand(HealingValue<AbstractCharacter> value)
        {
            var deltaHealing = currentHealth + value.healingValue;
            currentHealth = (deltaHealing >= maxHealth) ? maxHealth : deltaHealing;
        }

        private int Dead()
        {
            isCombat = false;
            isRetreat = false;
            isHunting = false;
            _deadAction?.Invoke();
            return 0;
        }

        public void SetReloadTime(float time)
        {
            _reloadTime = time + _reloadValue;
        }
        
        public bool CheckReloadTime(float time)
        {
            return time >= _reloadTime;
        }

        public bool CheckCurrentHealthToLimit(HealthStatus limit)
        {
            bool result = false;
            switch (limit)
            {
                case (HealthStatus.MaxHealth):
                    result = currentHealth >= maxHealth;
                    break;
                case (HealthStatus.MediumHealth):
                    result = currentHealth <= maxHealth * 0.5f;
                    break;
                case (HealthStatus.LowHealth):
                    result = currentHealth <= maxHealth * 0.25f;
                    break;
                default:
                    break;
            }
            return result;
        }

        public float CalculationCharacterSpeed(SpeedStatus speedStatus)
        {
            return maxSpeed * speedModificator[speedStatus];
        }

        public float CalculationSpeedModificator(SpeedStatus speedStatus)
        {
            return maxSpeed * speedModificator[speedStatus] / ConstSpeedModificator;
        }

        public float GetSpeedModificator()
        {
            float value = 0f;
            value = (float)speedStatus + 1;
            return value;
        }
    }
}
