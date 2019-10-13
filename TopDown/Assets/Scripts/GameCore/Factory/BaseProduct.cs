
namespace GameCore.Factory
{
    abstract class BaseProduct<T>
        where T: class
    {
        protected T owner;

        public BaseProduct(T owner)
        {
            this.owner = owner;
        }
        public abstract object CreateProduct();
    }
}
