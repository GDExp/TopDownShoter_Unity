namespace Character
{
    public interface ITakeDamage
    {
        void TakeDamage(int damage);
    }

    public interface ITakeHealing
    {
        void TakeHeal(int heal);
    }

    public class StatusController: ITakeDamage, ITakeHealing
    {
        private int _maxHealth;
        private int _maxEnergy;
        private int _currentHealth;
        private int _currentEnergy;
        
        private float _reloadValue;
        private float _reloadTime;
                
        public bool isCombat { get; set; }
        public bool isHunting { get; set; }
        public bool isRetreat { get; set; }


        public StatusController(CharacterValueSO valueSO)
        {
            _maxHealth = valueSO.characterHealth;
            _maxEnergy = valueSO.characterEnergy;
            _currentHealth = _maxHealth;
            _currentEnergy = _maxEnergy;

            _reloadValue = valueSO.characterReload;
        }

        public void TakeDamage(int damage)
        {
            var deltaDamage = _currentHealth - damage;
            _currentHealth = (deltaDamage > 0) ? deltaDamage : Dead();
        }

        private int Dead()
        {
            //TO DO - сделать смерть + анимацию
            return 0;
        }

        public void TakeHeal(int heal)
        {
            var deltaHeal = _currentHealth + heal;
            _currentHealth = (deltaHeal >= _maxHealth) ? _maxHealth : deltaHeal;
        }

        //test
        public void TestCallback()
        {
            CustomDebug.LogMessage(_currentHealth, DebugColor.green);
            CustomDebug.LogMessage(_currentEnergy, DebugColor.orange);
        }

        public void SetReloadTime(float time)
        {
            _reloadTime = time + _reloadValue;
        }
        
        public bool CheckReloadTime(float time)
        {
            return time >= _reloadTime;
        }
    }
}
