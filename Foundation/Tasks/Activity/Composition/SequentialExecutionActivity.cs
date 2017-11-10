using Swisstalk.Foundation.Tasks.Engine;

namespace Swisstalk.Foundation.Tasks.Activity.Composition
{
    public class SequentialExecutionActivity : ExecutionGroupActivity<SequentialExecutionEngine>
    {
        public SequentialExecutionActivity() : base()
        {
        }

        public SequentialExecutionActivity(params IActivity[] activities) : base(activities)
        {
        }
    }
}
