﻿using GameCore;

namespace Character
{
    public enum HealthStatus
    {
        MaxHealth,
        MediumHealth,
        LowHealth,
    }

    public class StatusController : IReceiver<DamageValue<AbstractCharacter>>, IReceiver<HealingValue<AbstractCharacter>>
    {
        public readonly int maxHealth;
        public readonly int maxEnergy;
        public readonly float maxSpeed;

        private int _currentHealth;
        private int _currentEnergy;
        
        private float _reloadValue;
        private float _reloadTime;
        private float _healingPower;

        public bool isRange;
        public bool isCombat;
        public bool isHunting;
        public bool isRetreat;


        public StatusController(CharacterValueSO valueSO)
        {
            maxHealth = valueSO.characterHealth;
            maxEnergy = valueSO.characterEnergy;
            maxSpeed = valueSO.characterSpeed;

            _currentHealth = maxHealth;
            _currentEnergy = maxEnergy;

            _reloadValue = valueSO.characterReload;

        }

        public void HandleCommand(DamageValue<AbstractCharacter> value)
        {
            if (isRetreat) isRetreat = false;
            var deltaDamage = _currentHealth - value.damageValue;
            _currentHealth = (deltaDamage > 0) ? deltaDamage : Dead();
        }

        public void HandleCommand(HealingValue<AbstractCharacter> value)
        {
            var deltaHealing = _currentHealth + value.healingValue;
            _currentHealth = (deltaHealing >= maxHealth) ? maxHealth : deltaHealing;
        }

        private int Dead()
        {
            //TO DO - сделать смерть + анимацию
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
                    result = _currentHealth >= maxHealth;
                    break;
                case (HealthStatus.MediumHealth):
                    result = _currentHealth <= maxHealth * 0.5f;
                    break;
                case (HealthStatus.LowHealth):
                    result = _currentHealth <= maxHealth * 0.25f;
                    break;
                default:
                    break;
            }
            return result;
        }

        public void RefreshHelth(ref int hp_visual)//test functions to visual
        {
            hp_visual = _currentHealth;
        }
    }
}
