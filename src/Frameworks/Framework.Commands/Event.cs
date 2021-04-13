using System;

namespace Framework.Commands
{
    public abstract class Event
    {
        public DateTime Timestamp { get; protected set; }
        public string ExchangeName { get; private set; }

        protected Event(string exchangeName)
        {
            ExchangeName = exchangeName;
            Timestamp = DateTime.Now;
        }
    }
}