using Swisstalk.Foundation.Tasks.Engine;

namespace Swisstalk.Foundation.Tasks.Activity.Composition
{
    public class ParallelExecutionActivity : ExecutionGroupActivity<ParallelExecutionEngine>
    {
        public ParallelExecutionActivity() : base()
        {
        }

        public ParallelExecutionActivity(params IActivity[] activities) : base(activities)
        {
        }
    }
}