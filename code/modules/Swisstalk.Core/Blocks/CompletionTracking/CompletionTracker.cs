using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Core.Blocks.Execution;
using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Tracking.Token;
using Swisstalk.Foundation.Tasks.Worker;

namespace Swisstalk.Core.Blocks.CompletionTracking
{
	public class CompletionTracker : IBlock, ICompletionTracker
    {
        private readonly BlockId _instanceId;
		private readonly IExecutor _executor;

        public CompletionTracker(BlockId instanceId, IExecutor executor)
        {
            _instanceId = instanceId;
			_executor = executor;
        }

        public BlockId InstanceId
        {
            get
            {
                return _instanceId;
            }
        }

        public IExecutionToken Track(IExecutionToken token, ISingleArgStatement<CompletionTrackingContext> completionStatement)
        {
            CompletionTrackingWorker trackingWorker = new CompletionTrackingWorker(token, completionStatement);
            WorkerTask task = new WorkerTask(trackingWorker);

			return _executor.Execute(task);
        }
    }
}
