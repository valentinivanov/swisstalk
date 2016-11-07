using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.NotificationQueue;
using Swisstalk.Foundation.Factory;
using Swisstalk.Foundation.Pooling.ObjectPool;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    internal struct NotificationTopicFactoryState
    {
        public readonly TopicId TopicId;
        public readonly IQueueNotifier QueueNotifier;

        public NotificationTopicFactoryState(TopicId topicId, IQueueNotifier queueNotifier)
        {
            TopicId = topicId;
            QueueNotifier = queueNotifier;
        }
    }

    internal class NotificationTopicFactory : IFactory<NotificationTopic>, IPooledObject<NotificationTopicFactoryState>
    {
        private NotificationTopicFactoryState _state;

        public NotificationTopicFactory()
        {
        }

        public NotificationTopic Execute()
        {
            return new NotificationTopic(_state.TopicId, _state.QueueNotifier);
        }

        public void Construct(NotificationTopicFactoryState state)
        {
            _state = state;
        }

        public void Construct()
        {
            Reset();
        }

        public void Reset()
        {
            _state = new NotificationTopicFactoryState(null, null);
        }
    }
}
