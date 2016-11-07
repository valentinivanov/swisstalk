using System.Collections.Generic;
using Swisstalk.Foundation.Algorithms;
using Swisstalk.Foundation.Factory.Object;
using Swisstalk.Foundation.Collections.Observable;
using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.NotificationQueue;
using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    internal class NotificationTopic : INotificationTopic
    {
        private static readonly ObjectFactory<ObservableCollection<ObserverBucket>> DefaultObservableFactory = new ObjectFactory<ObservableCollection<ObserverBucket>>();

        private TopicId _name;
        private IQueueNotifier _notifier;
        private Dictionary<NotificationId, ObservableCollection<ObserverBucket>> _observers;

        private KeySearchMethod<NotificationId, ObservableCollection<ObserverBucket>> _observerQuery;

        public NotificationTopic(TopicId name, IQueueNotifier queueNotifier)
        {
            _name = name;

            _notifier = queueNotifier;
            _observers = new Dictionary<NotificationId, ObservableCollection<ObserverBucket>>();

            _observerQuery = new KeySearchMethod<NotificationId, ObservableCollection<ObserverBucket>>();
        }

        public TopicId Id
        {
            get
            {
                return _name;
            }
        }

        public IObservationToken Observe(NotificationId notificationId, INotificationObserver observer)
        {
            ObservableCollection<ObserverBucket> buckets = _observerQuery.GetOrCreate(notificationId, _observers, DefaultObservableFactory);
            ObserverBucket observerBucket = new ObserverBucket(observer);
            buckets.Add(observerBucket);

            return new ObservationToken(buckets, observerBucket);
        }

        public void Post(NotificationId notificationId, NotificationArgs args)
        {
            _notifier.Enqueue(this, notificationId, args);
        }

        public void Send(NotificationId notificationId, NotificationArgs args)
        {
            ObservableCollection<ObserverBucket> buckets = _observerQuery.GetOrCreate(notificationId, _observers, DefaultObservableFactory);

            using (ChangeAwareEnumerator<ObserverBucket> cae = new ChangeAwareEnumerator<ObserverBucket>(buckets))
            {
                while (cae.MoveNext())
                {
                    ObserverBucket currentItem = cae.Current;
                    currentItem.Observer.Execute(args);
                }
            }
        }
    }
}
