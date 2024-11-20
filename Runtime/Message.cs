namespace PubSub.Runtime
{
    using System;
    using System.Collections.Generic;

    public class Message<T> : IMessenger<T>
    {
        private readonly HashSet<Delegate> subscriberCallback = new();

#region Internal

        private void InternalSubscribe(Delegate @delegate)
        {
            if (this.subscriberCallback.TryGetValue(@delegate, out _))
            {
                throw new InvalidOperationException($"Has already subscribed to {@delegate.Method.Name}");
            }

            this.subscriberCallback.Add(@delegate);
        }

        private void InternalUnsubscribe(Delegate @delegate)
        {
            if (!this.subscriberCallback.TryGetValue(@delegate, out _))
            {
                throw new InvalidOperationException($"Has not subscribed to {@delegate.Method.Name}");
            }

            this.subscriberCallback.Remove(@delegate);
        }

#endregion

        public void Subscribe(Action<T> callback)
        {
            this.InternalSubscribe(callback);
        }

        public void Subscribe(Action callback)
        {
            this.InternalSubscribe(callback);
        }

        public void Unsubscribe(Action<T> callback)
        {
            this.InternalUnsubscribe(callback);
        }

        public void Unsubscribe(Action callback)
        {
            this.InternalSubscribe(callback);
        }

        public void Publish(T value)
        {
            foreach (var callback in this.subscriberCallback)
            {
                callback.DynamicInvoke(value);
            }
        }

        public void Dispose()
        {
            this.subscriberCallback.Clear();
        }
    }
}