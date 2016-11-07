using Swisstalk.Foundation.Tasks.Engine;

namespace Swisstalk.Foundation.Tasks.Tracking.Token
{
    public struct CompletionTrackingContext
    {
        private readonly IExecutionToken _token;

        public CompletionTrackingContext(IExecutionToken token)
        {
            _token = token;
        }

        public IExecutionToken Token
        {
            get
            {
                return _token;
            }
        }
    }
}
