using Swisstalk.Foundation.Pooling.Scope.Disposable;

namespace Swisstalk.Foundation.Tasks.Token
{
    public class CompositeExecutionToken : DisposableScopeFrame, IExecutionToken
    {
        public CompositeExecutionToken() : base()
        {
        }

        public CompositeExecutionToken(params IExecutionToken[] tokens) : base()
        {
            foreach (IExecutionToken token in tokens)
            {
                Append(token);
            }
        }

        public TaskState State
        {
            get
            {
                return GetEffectiveTaskState();
            }
        }

        private TaskState GetEffectiveTaskState()
        {
            TaskState effectiveState = TaskStateExtension.GetMaxStateValue();

            foreach (IExecutionToken item in Composite)
            {
                if (item.State < effectiveState)
                {
                    effectiveState = item.State;
                }
            }

            return effectiveState;
        }
    }
}
