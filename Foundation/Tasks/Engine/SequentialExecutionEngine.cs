using Swisstalk.Foundation.Behaviors;
using Swisstalk.Foundation.Metadata;
using Swisstalk.Foundation.Tasks.Activity;
using Swisstalk.Foundation.Tasks.Token;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Tasks.Engine
{
    public class SequentialExecutionEngine : IExecutionEngine, IRemovable<ITask>, IDisposable
    {
        private readonly List<ITask> _executionQueue;

        private SuspendableState _state;
        private ITask _current;

        public SequentialExecutionEngine()
        {
            _executionQueue = new List<ITask>();

            _current = null;
            _state = SuspendableState.Active;
        }
        
        bool IRemovable<ITask>.Remove(ITask item)
        {
            if (_current == item)
            {
                item.Stop();
                item.Dispose();
                _current = null;
                return true;
            }
            else if (_executionQueue.Contains(item))
            {
                item.Dispose();

                return _executionQueue.Remove(item);
            }

            return false;
        }

        public IExecutionToken Execute(ITask task)
        {
            _executionQueue.Add(task);
            return new TaskExecutionToken(task, this);
        }

        public IExecutionToken Execute(IActivity activity)
        {
            return Execute(new ActivityTask(activity));
        }

        public void Suspend()
        {
            _state = SuspendableState.Suspended;
        }

        public void Resume()
        {
            _state = SuspendableState.Active;
        }

        public bool IsActive()
        {
            return (_state == SuspendableState.Active);
        }

        public void Update(TimeSpan delta)
        {
            if (IsActive())
            {
                UpdateExecutionState(delta);

                while (_executionQueue.Count > 0 && _current == null)
                {
                    UpdateExecutionState(delta);
                }
            }
        }

        private void UpdateExecutionState(TimeSpan delta)
        {
            if (_current == null)
            {
                if (_executionQueue.Count > 0)
                {
                    _current = _executionQueue.Dequeue();

                    _current.Start();
                    if (_current != null)
                    {
                        UpdateCurrentTask(delta);
                    }
                }
            }
            else
            {
                UpdateCurrentTask(delta);
            } 
        }

        private void UpdateCurrentTask(TimeSpan delta)
        {
            _current.Update(delta);

            if (_current != null && !_current.State.IsActive())
            {
                _current.Stop();
                _current.DisposeIfNotNull();
                _current = null;
            }
        }

		public void Dispose()
		{
			if (_current != null && _current.State != TaskState.Disposed)
			{
				_current.Dispose();
				_current = null;
			}

            List<ITask> queueCopy = new List<ITask>(_executionQueue);
            foreach (ITask task in queueCopy)
			{
				task.Dispose();
			}

			_executionQueue.Clear();
		}
	}
}
