using System;
using Swisstalk.Core.Blocks.Notifications.Observation;
using Swisstalk.Foundation.Collections.Observable;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    internal class ObservationToken : IObservationToken
    {
        private readonly ObservableCollection<ObserverBucket> _targetCollection;
        private readonly ObserverBucket _targetObject;

        public ObservationToken(ObservableCollection<ObserverBucket> targetCollection, ObserverBucket targetObject)
        {
            _targetCollection = targetCollection;
            _targetObject = targetObject;
        }

        public void Dispose()
        {
            if (_targetCollection.Contains(_targetObject))
            {
                _targetCollection.Remove(_targetObject);
            }
            else 
            {
                throw new InvalidOperationException("SubscriptionToken: collection has no corresponding observer");
            }
        }
    }
}
