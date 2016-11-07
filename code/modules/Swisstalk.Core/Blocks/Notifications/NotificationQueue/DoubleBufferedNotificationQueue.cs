using System.Collections.Generic;
using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;
using Swisstalk.Foundation.Algorithms;

namespace Swisstalk.Core.Blocks.Notifications.NotificationQueue
{
    internal class DoubleBufferedNotificationQueue : IQueueNotifier
    {
        private List<NotificationItem> _activeQueue;
        private List<NotificationItem> _pendingQueue;

        private IterationMethod<NotificationItem> _iterationMethod;
        private NotificationSendStatement _sendStatement;

        public DoubleBufferedNotificationQueue()
        {
            _activeQueue = new List<NotificationItem>();
            _pendingQueue = new List<NotificationItem>();

            _iterationMethod = new IterationMethod<NotificationItem>();
            _sendStatement = new NotificationSendStatement();
        }

        public void Enqueue(INotificationTopic topic, NotificationId notificationId, NotificationArgs args)
        {
            _pendingQueue.Add(new NotificationItem(topic, notificationId, args));
        }

        public void DispatchMessages()
        {
            SwapActive();

            _iterationMethod.ForEachItem(_activeQueue, _sendStatement);

            _activeQueue.Clear();
        }

        private void SwapActive()
        {
            var temp = _pendingQueue;
            _pendingQueue = _activeQueue;
            _activeQueue = temp;
        }
    }
}
