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

    class StatusController: ITakeDamage, ITakeHealing
    {
        private int _maxHealth;
        private int _maxEnergy;
        private int _currentHealth;
        private int _currentEnergy;


        public StatusController(int health, int energy)
        {
            _maxHealth = health;
            _maxEnergy = energy;
            _currentHealth = _maxHealth;
            _currentEnergy = _maxEnergy;
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
        
    }
}
