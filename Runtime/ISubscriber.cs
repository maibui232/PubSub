namespace PubSub.Runtime
{
    using System;

    public interface ISubscriber : IDisposable
    {
    }

    public interface ISubscriber<T> : ISubscriber
    {
        void Subscribe(Action<T>   callback);
        void Subscribe(Action      callback);
        void Unsubscribe(Action<T> callback);
        void Unsubscribe(Action    callback);
    }
}