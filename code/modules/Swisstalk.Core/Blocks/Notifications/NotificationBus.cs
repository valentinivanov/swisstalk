using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Algorithms;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Pooling.ObjectPool;
using Swisstalk.Foundation.Pooling.ObjectPool.Linear;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Core.Blocks.Notifications.NotificationQueue;
using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Topic;

namespace Swisstalk.Core.Blocks.Notifications
{
    public class NotificationBus : INotificationBus, IActiveObject, IDisposable
    {
        private const int FactoryPoolCapacity = 16;

        private readonly BlockId _instanceId;

        private readonly DoubleBufferedNotificationQueue _notificationQueue;
        private readonly Dictionary<TopicId, NotificationTopic> _topics;

        private readonly KeySearchMethod<TopicId, NotificationTopic> _topicQuery;

        private readonly IObjectPool<NotificationTopicFactory, NotificationTopicFactoryState> _topicFactoryPool;

        public NotificationBus(BlockId instanceId)
        {
            _instanceId = instanceId;

            _notificationQueue = new DoubleBufferedNotificationQueue();
            _topics = new Dictionary<TopicId, NotificationTopic>();

            _topicQuery = new KeySearchMethod<TopicId, NotificationTopic>();

            _topicFactoryPool = LinearObjectPoolEmitter<NotificationTopicFactory, NotificationTopicFactoryState>.NewDynamicPool(FactoryPoolCapacity);
        }

        public BlockId InstanceId
        {
            get
            {
                return _instanceId;
            }
        }

        public void Update(TimeSpan delta)
        {
            _notificationQueue.DispatchMessages();
        }

        public INotificationTopic GetTopic(TopicId topicId)
        {
            using (var factoryPoolRef = _topicFactoryPool.Get(new NotificationTopicFactoryState(topicId, _notificationQueue)))
            {
                return _topicQuery.GetOrCreate(topicId, _topics, factoryPoolRef.Instance);
            }
        }

        public void Dispose()
        {
            _topicFactoryPool.Dispose();
        }
    }
}
