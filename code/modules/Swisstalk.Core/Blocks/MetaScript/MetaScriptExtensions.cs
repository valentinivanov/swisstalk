using Swisstalk.Core.Blocks.CompletionTracking;
using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Tracking.Token;
using Swisstalk.Foundation.Tasks.Worker;

namespace Swisstalk.Core.Blocks.MetaScript
{
	public static class MetaScriptExtensions
	{
		public static IExecutionToken Execute(this IExecutionEngine engine, IMetaScriptStatement statement)
		{
			ITask task = new WorkerTask(statement);
			return engine.Execute(task);
		}

		public static void Execute(this IExecutionEngine engine, 
		                           IMetaScriptStatement statement, 
		                           ICompletionTracker completionTracker, 
		                           ISingleArgStatement<CompletionTrackingContext> completionCommand)
		{
			ITask task = new WorkerTask(statement);
			IExecutionToken token = engine.Execute(task);
			completionTracker.Track(token, completionCommand);
		}
	}
}
