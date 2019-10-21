namespace GameCore.Strategy
{
    public class StrategySwithcer
    {
        private IStrategy _currentStrategy;

        public StrategySwithcer(IStrategy currentStrategy = null)
        {
            _currentStrategy = currentStrategy;
        }

        public void SetStrategy(IStrategy newStrategy)
        {
            if (_currentStrategy == newStrategy) return;
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
