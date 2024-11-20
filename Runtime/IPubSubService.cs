namespace PubSub.Runtime
{
    using System;
    using System.Collections.Generic;

    public interface IPubSubService
    {
        void Subscribe<T>(Action<T>   callback);
        void Subscribe<T>(Action      callback);
        void Unsubscribe<T>(Action<T> callback);
        void Unsubscribe<T>(Action    callback);
        void Publish<T>(T             message);
    }

    public class PubSubService : IPubSubService
    {
        private readonly Dictionary<Type, IMessenger> typeToSubscriber = new();

#region IPubSubService Implementation

        public void Subscribe<T>(Action<T> callback)
        {
            this.GetMessenger<T>().Subscribe(callback);
        }

        public void Subscribe<T>(Action callback)
        {
            this.GetMessenger<T>().Subscribe(callback);
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            this.GetMessenger<T>().Unsubscribe(callback);
        }

        public void Unsubscribe<T>(Action callback)
        {
            this.GetMessenger<T>().Unsubscribe(callback);
        }

        public void Publish<T>(T message)
        {
            this.GetMessenger<T>().Publish(message);
        }

#endregion

#region Private

        private IMessenger<T> GetMessenger<T>()
        {
            var           type = typeof(T);
            IMessenger<T> messenger;
            if (this.typeToSubscriber.TryGetValue(type, out var cacheSubscriber))
            {
                messenger = (IMessenger<T>)cacheSubscriber;
            }
            else
            {
                messenger = new Message<T>();
                this.typeToSubscriber.Add(type, messenger);
            }

            return messenger;
        }

#endregion
    }
}