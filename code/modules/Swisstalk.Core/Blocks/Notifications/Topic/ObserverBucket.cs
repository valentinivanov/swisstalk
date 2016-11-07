using Swisstalk.Core.Blocks.Notifications.Observation;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    internal class ObserverBucket
    {
        private INotificationObserver _observer;

        public ObserverBucket(INotificationObserver observer)
        {
            _observer = observer;
        }

        public INotificationObserver Observer
        {
            get
            {
                return _observer;
            }
        }
    }
}
