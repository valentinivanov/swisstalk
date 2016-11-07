using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications.NotificationQueue
{
    internal struct NotificationItem
    {
        private INotificationTopic _topic;
        private NotificationId _id;
        private NotificationArgs _args;

        public NotificationItem(INotificationTopic topic,
                                NotificationId notificationId,
                                NotificationArgs args)
        {
            _topic = topic;
            _id = notificationId;
            _args = args;
        }

        public INotificationTopic Topic
        {
            get 
            {
                return _topic;
            }
        }

        public NotificationId Id
        {
            get
            {
                return _id;
            }
        }

        public NotificationArgs Args
        {
            get
            {
                return _args;
            }
        }
    }

}
