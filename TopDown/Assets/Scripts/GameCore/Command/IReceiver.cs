namespace GameCore
{
    interface IReceiver<T>
    {
        void HandleCommand(T obj);
    }
}
