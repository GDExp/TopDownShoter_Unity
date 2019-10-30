using GameCore;

namespace Character
{
    public class StatusController : IReceiver<DamageValue<AbstractCharacter>>
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
            var deltaDamage = _currentHealth - value.damageValue;
            _currentHealth = (deltaDamage > 0) ? deltaDamage : Dead();
            CustomDebug.LogMessage(_currentHealth, DebugColor.blue);
        }

        private int Dead()
        {
            //TO DO - сделать смерть + анимацию
            return 0;
        }

        public void TakeHeal(int heal)
        {
            var deltaHeal = _currentHealth + heal;
            _currentHealth = (deltaHeal >= maxHealth) ? maxHealth : deltaHeal;
        }

        public void SetReloadTime(float time)
        {
            _reloadTime = time + _reloadValue;
        }
        
        public bool CheckReloadTime(float time)
        {
            return time >= _reloadTime;
        }

        public bool CheckCurrentHealthIsMax()
        {
            return _currentHealth >= maxHealth;
        }

        public bool ChechCurrentHealthToLimit(int limit)
        {
            return _currentHealth <= limit;
        }

        public void RefreshHelth(ref int hp_visual)//test functions to visual
        {
            hp_visual = _currentHealth;
        }
    }
}
