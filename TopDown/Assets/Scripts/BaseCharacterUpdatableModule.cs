namespace GameCore
{
    public class BaseCharacterUpdatableModule<T> : IUpdatableModule
        where T: Character.AbstractCharacter
    {
        protected T owner;

        public BaseCharacterUpdatableModule(T owner)
        {
            this.owner = owner;
        }

        public virtual void OnStart()
        {

        }

        public virtual void OnUpdate()
        {
        }
    }
}
