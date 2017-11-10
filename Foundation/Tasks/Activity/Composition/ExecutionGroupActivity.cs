using Swisstalk.Foundation.Tasks.Token;
using System;

namespace Swisstalk.Foundation.Tasks.Activity.Composition
{
    public class ExecutionGroupActivity<T> : IActivity
        where T : IExecutionEngine, new()
    {
        private readonly IExecutionEngine _engine;
        protected readonly CompositeExecutionToken _compositeToken;

        public ExecutionGroupActivity()
        {
            _engine = new T();
            _compositeToken = new CompositeExecutionToken();
        }

        public ExecutionGroupActivity(params IActivity[] activities) : this()
        {
            Append(activities);
        }

        public void Append(IActivity[] activities)
        {
            foreach (IActivity activity in activities)
            {
                Append(activity);
            }
        }

        public void Append(IActivity activity)
        {
            IExecutionToken token = _engine.Execute(activity);
            _compositeToken.Append(token);
        }

        public void Start()
        {
        }

        public void Stop()
        {
            if (_compositeToken.State.IsActive())
            {
                _compositeToken.Cancel();
            }
        }

        public bool Update(TimeSpan delta)
        {
            _engine.Update(delta);
            return IsTokenComplete();
        }

        protected virtual bool IsTokenComplete()
        {
            return _compositeToken.State.IsComplete();
        }

        public void Dispose()
        {
            _compositeToken.Dispose();
        }
    }
}
