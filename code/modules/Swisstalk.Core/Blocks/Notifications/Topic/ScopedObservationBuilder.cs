using System;
using Swisstalk.Core.Blocks.Notifications.Metadata;
using Swisstalk.Core.Blocks.Notifications.Observation;
using Swisstalk.Foundation.Pooling.Scope;
using Swisstalk.Foundation.Pooling.Scope.Disposable;

namespace Swisstalk.Core.Blocks.Notifications.Topic
{
    public class ScopedObservationBuilder
    {
        private Scope<DisposableScopeFrame> _scope;
        private ObservationBuilder _builder;

        public ScopedObservationBuilder(INotificationBus service, Scope<DisposableScopeFrame> scope)
        {
            _scope = scope;
            _builder = new ObservationBuilder(service);
        }

        public ScopedObservationBuilder Observe(TopicId topic, NotificationId notification, INotificationObserver observer)
        {
            _builder.Observe(topic, notification, observer);

            return this;
        }

        public void Build()
        {
            if (_scope.IsEmpty())
            {
                throw new InvalidOperationException("Cannot build with empty scope!");
            }

            _builder.Build().AutoScope(_scope);
        }
    }
}
