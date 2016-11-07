using System;
using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks.Worker;

namespace Swisstalk.Foundation.Tasks.Tracking.Token
{
    public class CompletionTrackingWorker : ITaskWorker
    {
        private readonly IExecutionToken _token;
        private readonly ISingleArgStatement<CompletionTrackingContext> _callbackStatement;

        private bool _callbackFired;

        public CompletionTrackingWorker(IExecutionToken token,
                                      ISingleArgStatement<CompletionTrackingContext> callbackStatement)
        {
            _token = token;
            _callbackStatement = callbackStatement;

            _callbackFired = false;
        }

        public void Dispose()
        {
            EvaluateState(_token.State);
        }

        public void Start()
        {
            EvaluateState(_token.State);
        }

        public void Stop()
        {
            EvaluateState(_token.State);
        }

        public bool Update(TimeSpan delta)
        {
            return EvaluateState(_token.State);
        }

        private bool EvaluateState(TaskState currentState)
        {
            if (!_callbackFired && currentState == TaskState.Disposed)
            {
                CompletionTrackingContext args = new CompletionTrackingContext(_token);
                _callbackStatement.Execute(args);

                _callbackFired = true;
            }

            return _callbackFired;
        }
    }
}
