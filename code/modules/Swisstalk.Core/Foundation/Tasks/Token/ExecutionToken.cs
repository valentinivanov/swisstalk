using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Tasks.Token
{
    public class ExecutionToken : IExecutionToken
    {
        private readonly ITask _task;
        private readonly IRemoveable<ITask> _remover;

        private bool _alive;

        public ExecutionToken(ITask task, IRemoveable<ITask> remover)
        {
            _task = task;
            _remover = remover;
            _alive = true;
        }

        public TaskState State
        {
            get
            {
                return _task.State;
            }
        }

        public void Dispose()
        {
            if (_alive)
            {
                _remover.Remove(_task);
                _alive = false;
            }
        }
    }
}
