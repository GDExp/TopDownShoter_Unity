namespace GameCore
{
    public struct DamageValue<T>
        where T: Character.AbstractCharacter
    {
        public readonly T damageOwner;
        public readonly int damageValue;

        public DamageValue(T damageOwner, int damageValue)
        {
            this.damageOwner = damageOwner;
            this.damageValue = damageValue;
        }
    }
}

