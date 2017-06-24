namespace Fibon.Messages.Events
{
    public class ValueCalculatedEvent : IEvent
    {
        public int Number { get; }

        public int Result { get; }

        public ValueCalculatedEvent(int number, int result)
        {
            Number = number;
            Result = result;
        }

        protected ValueCalculatedEvent()
        {
        }
    }
}