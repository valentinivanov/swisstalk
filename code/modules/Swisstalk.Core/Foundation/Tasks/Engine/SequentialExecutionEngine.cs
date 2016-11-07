using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Algorithms;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Tasks.Token;

namespace Swisstalk.Foundation.Tasks.Engine
{
    public class SequentialExecutionEngine : IExecutionEngine, IRemoveable<ITask>, IDisposable
    {
        private readonly List<ITask> _executionQueue;
        private readonly ExecutionEngineId _id;

        private SuspendableState _state;
        private ITask _current;

        public SequentialExecutionEngine(ExecutionEngineId engineId)
        {
            _executionQueue = new List<ITask>();
            _id = engineId;

            _current = null;
            _state = SuspendableState.Active;
        }
        
        void IRemoveable<ITask>.Remove(ITask item)
        {
            if (_current == item)
            {
                item.Stop();
                item.Dispose();
                _current = null;
            }
            else if (_executionQueue.Contains(item))
            {
                item.Dispose();

                _executionQueue.Remove(item);
            }
        }

        public IExecutionToken Execute(ITask task)
        {
            _executionQueue.Add(task);
            return new ExecutionToken(task, this);
        }

        public ExecutionEngineId Id
        {
            get
            {
                return _id;
            }
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
                    UpdateCurrentTask(delta);
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

            if (!_current.State.IsActive())
            {
                _current.Stop();
                _current.Dispose();
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

			foreach (ITask task in _executionQueue)
			{
				task.Dispose();
			}

			_executionQueue.Clear();
		}
	}
}
