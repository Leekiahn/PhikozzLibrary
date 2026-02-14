using System;

namespace PhikozzLibrary
{
    public interface IEventService
    {
        public void Subscribe<T>(Action<T> onEvent) where T : IGameEvent;
        public void Unsubscribe<T>(Action<T> onEvent) where T : IGameEvent;
        public void Publish<T>(T eventData) where T : IGameEvent;
    }
}
