using System;
using System.Collections.Generic;
using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    public class ObservationBuilder
    {
        private struct ObservationItem
        {
            public TopicId Topic;
            public NotificationId Notification;
            public INotificationObserver Observer;

            public ObservationItem(TopicId topic, 
                                   NotificationId notification, 
                                   INotificationObserver observer)
            {
                Topic = topic;
                Notification = notification;
                Observer = observer;
            }
        }

        private readonly INotificationBus _service;
        private readonly List<ObservationItem> _items;

        public ObservationBuilder(INotificationBus service)
        {
            _service = service;
            _items = new List<ObservationItem>();
        }

        public ObservationBuilder Observe(TopicId topic, NotificationId notification, INotificationObserver observer)
        {
            _items.Add(new ObservationItem(topic, notification, observer));
            
            return this;
        }

        public IObservationToken Build()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("ObservationBuilder: no observation items to build!");
            }

            CompositeObservationToken compositeToken = new CompositeObservationToken();

            foreach (ObservationItem item in _items)
            {
                INotificationTopic topic = _service.GetTopic(item.Topic);
                IObservationToken token = topic.Observe(item.Notification, item.Observer);

                compositeToken.Append(token);
            }

            //it's not allowed to call Build twice in a row
            _items.Clear();

            return compositeToken;
        }
    }
}
