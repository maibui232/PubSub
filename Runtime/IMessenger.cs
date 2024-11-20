namespace PubSub.Runtime
{
    using System;

    public interface IMessenger : IDisposable
    {
    }

    public interface IMessenger<T> : IMessenger
    {
        void Subscribe(Action<T>   callback);
        void Subscribe(Action      callback);
        void Unsubscribe(Action<T> callback);
        void Unsubscribe(Action    callback);
        void Publish(T             value);
    }
}