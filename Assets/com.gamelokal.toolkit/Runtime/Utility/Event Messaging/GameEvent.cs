namespace GameLokal.Utility.Event
{
    public struct GameEvent
    {
        public string eventName;

        public GameEvent(string newName)
        {
            eventName = newName;
        }

        static GameEvent e;

        public static void Trigger(string newName)
        {
            e.eventName = newName;
            EventManager.TriggerEvent(e);
        }
    }
}