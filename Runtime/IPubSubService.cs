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
        private readonly Dictionary<Type, ISubscriber> typeToSubscriber = new();

#region IPubSubService Implementation

        public void Subscribe<T>(Action<T> callback)
        {
            this.GetSubscriber<T>().Subscribe(callback);
        }

        public void Subscribe<T>(Action callback)
        {
            this.GetSubscriber<T>().Subscribe(callback);
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            this.GetSubscriber<T>().Unsubscribe(callback);
        }

        public void Unsubscribe<T>(Action callback)
        {
            this.GetSubscriber<T>().Unsubscribe(callback);
        }

        public void Publish<T>(T message)
        {
            throw new NotImplementedException();
        }

#endregion

#region Private

        private ISubscriber<T> GetSubscriber<T>()
        {
            var            type = typeof(T);
            ISubscriber<T> subscriber;
            if (this.typeToSubscriber.TryGetValue(type, out var cacheSubscriber))
            {
                subscriber = (ISubscriber<T>)cacheSubscriber;
            }
            else
            {
                subscriber = new Message<T>();
                this.typeToSubscriber.Add(type, subscriber);
            }

            return subscriber;
        }

#endregion
    }
}