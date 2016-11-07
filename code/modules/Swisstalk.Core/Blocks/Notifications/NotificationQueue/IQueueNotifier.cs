using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications.NotificationQueue
{
    internal interface IQueueNotifier
    {
        void Enqueue(INotificationTopic topic, NotificationId notificationId, NotificationArgs args);
    }
}
