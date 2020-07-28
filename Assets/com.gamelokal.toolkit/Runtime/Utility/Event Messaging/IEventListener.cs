namespace GameLokal.Utility.Event
{
    public interface IEventListener<T> : IEventListenerBase
    {
        void OnEvent(T eventType);
    }
}