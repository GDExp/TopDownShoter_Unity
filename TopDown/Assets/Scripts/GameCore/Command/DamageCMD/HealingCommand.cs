using Character;

namespace GameCore
{
    class HealingCommand : PatternCommand<AbstractCharacter>
    {
        private IReceiver<HealingValue<AbstractCharacter>> _receiver;

        private readonly HealingValue<AbstractCharacter> _healingValue;

        public HealingCommand(AbstractCharacter invoker, int value) : base(invoker)
        {
            _receiver = invoker?.StatusController;

            _healingValue = new HealingValue<AbstractCharacter>(invoker, value);
        }

        public override void Execute()
        {
            _receiver.HandleCommand(_healingValue);
        }
    }
}
