namespace GameCore
{
    public interface IReceiver<T>
    {
        void HandleCommand(T obj);
    }
}
