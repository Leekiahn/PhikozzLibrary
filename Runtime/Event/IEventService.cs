using System;

namespace PhikozzLibrary
{
    public interface IEventService
    {
        public void Subscribe<T>(Action<T> onEvent);
        public void Unsubscribe<T>(Action<T> onEvent);
        public void UnSubscribeAll<T>();
        public void Publish<T>(T eventData);
    }
}
