using Swisstalk.Foundation.Metadata;
using System;

namespace Swisstalk.Foundation.Tasks.Token
{
    public class TaskExecutionToken : IExecutionToken, IDisposable
    {
        private readonly ITask _task;
        private readonly IRemovable<ITask> _remover;

        private TokenState _currentState;

        public TaskExecutionToken(ITask task, IRemovable<ITask> remover)
        {
            _task = task;
            _remover = remover;
            _currentState = TokenState.Undefined;
        }

        public TokenState State
        {
            get
            {
                return (_currentState == TokenState.Undefined) ? _task.State.ToTokenState() : _currentState;
            }
        }

        public void Cancel()
        {
            TryRemoveTaskFromExecution();
            _currentState = TokenState.Cancelled;
        }

        public void Dispose()
        {
            TryRemoveTaskFromExecution();
            _currentState = TokenState.Done;
        }

        private void TryRemoveTaskFromExecution()
        {
            if (_currentState == TokenState.Undefined)
            {
                _remover.Remove(_task);
            }
        }
    }
}
