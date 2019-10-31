namespace GameCore
{
    public struct HealingValue<T>
        where T : Character.AbstractCharacter
    {
        public readonly T healingOwner;
        public readonly int healingValue;

        public HealingValue(T healingOwner, int healingValue)
        {
            this.healingOwner = healingOwner;
            this.healingValue = healingValue;
        }
    }
}
