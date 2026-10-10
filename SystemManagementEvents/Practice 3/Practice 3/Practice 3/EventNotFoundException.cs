using System;

namespace Practice_3
{
    internal class EventNotFoundException : Exception
    {
        public EventNotFoundException(string message) : base(message)
        {
                
        }
    }
    internal class InvalidEventStatusException : Exception
    {
        public InvalidEventStatusException(string message)
            : base(message)
        {
        }
    }
}