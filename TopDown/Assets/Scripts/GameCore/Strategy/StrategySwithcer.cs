namespace GameCore.Strategy
{
    class StrategySwithcer
    {
        private IStrategy _currentStrategy;

        public StrategySwithcer(IStrategy currentStrategy = null)
        {
            _currentStrategy = currentStrategy;
        }

        public void SetStrategy(IStrategy newStrategy)
        {
            _currentStrategy = newStrategy;
        }

        public IStrategy GetStrategy()
        {
            return _currentStrategy;
        }

        public void StrategyIsWork()
        {
            _currentStrategy.DoStrategy();
        }

    }
}
