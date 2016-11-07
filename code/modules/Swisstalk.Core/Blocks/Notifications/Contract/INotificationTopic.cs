using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications
{
    public interface INotificationTopic
    {
        TopicId Id { get; }

        IObservationToken Observe(NotificationId notificationId, INotificationObserver observer);

        void Send(NotificationId notificationId, NotificationArgs args);
        void Post(NotificationId notificationId, NotificationArgs args);
    }
}
