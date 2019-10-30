using Character;

namespace GameCore
{
    class AttackDamageCommand : PatternCommand<AbstractCharacter>
    {
        private IReceiver<DamageValue<AbstractCharacter>> _receiver;
        private int _damage;

        public AttackDamageCommand(AbstractCharacter invoker, int damageValue ) : base(invoker)
        {
            _receiver = invoker.GetStatusController() as StatusController;
            _damage = damageValue;
        }

        public override void Execute()
        {
            var attackArg = new DamageValue<AbstractCharacter>(_invoker, _damage);
            _receiver?.HandleCommand(attackArg);
        }
    }
}
