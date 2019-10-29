namespace GameCore
{
    public class BaseUpdatableModule<T> : IUpdatableModule
        where T: Character.AbstractCharacter
    {
        protected T owner;

        public BaseUpdatableModule(T owner)
        {
            this.owner = owner;
        }

        public virtual void OnUpdate()
        {
        }
    }
}
