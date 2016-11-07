using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Tracking.Token;

namespace Swisstalk.Core.Blocks.CompletionTracking
{
	public interface ICompletionTracker
	{
		IExecutionToken Track(IExecutionToken token, ISingleArgStatement<CompletionTrackingContext> completionStatement);
	}
}
