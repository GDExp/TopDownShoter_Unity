using Character;

namespace GameCore
{
    class AttackDamageCommand : PatternCommand<AbstractCharacter>
    {
        private IReceiver<DamageValue<AbstractCharacter>> _receiver;
        private readonly DamageValue<AbstractCharacter> _damageValue;

        public AttackDamageCommand(AbstractCharacter invoker, AbstractCharacter owner, int value ) : base(invoker)
        {
            _receiver = invoker?.StatusController;
            _damageValue = new DamageValue<AbstractCharacter>(owner, value);
        }

        public override void Execute()
        {
            _receiver.HandleCommand(_damageValue);
        }
    }
}
