using Swisstalk.Foundation.Tasks.Engine;

namespace Swisstalk.Foundation.Tasks.Activity.Composition
{
    public class OneOfManyExecutionActivity : ExecutionGroupActivity<ParallelExecutionEngine>
    {
        public OneOfManyExecutionActivity() : base()
        {
        }

        public OneOfManyExecutionActivity(params IActivity[] activities) : base(activities)
        {
        }

        protected override bool IsTokenComplete()
        {
            return _compositeToken.IsAnyCompleted();
        }
    }
}