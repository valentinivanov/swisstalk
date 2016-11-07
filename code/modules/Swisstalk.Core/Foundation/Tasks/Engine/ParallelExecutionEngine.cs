using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Tasks.Token;

namespace Swisstalk.Foundation.Tasks.Engine
{
    public class ParallelExecutionEngine : IExecutionEngine, IRemoveable<ITask>, IDisposable
    {
        private readonly ExecutionEngineId _id;
        private readonly List<ITask> _tasks;
        private readonly List<ITask> _additionalTasks;

        private SuspendableState _state;

        public ParallelExecutionEngine(ExecutionEngineId engineId)
        {
            _id = engineId;
            _state = SuspendableState.Active;

            _tasks = new List<ITask>();
            _additionalTasks = new List<ITask>();
        }

        void IRemoveable<ITask>.Remove(ITask item)
        {
            if (!TryCleanupTask(item, _tasks))
            {
                if (TryCleanupTask(item, _additionalTasks))
                {
                    _additionalTasks.Remove(item);
                }
            }
        }

        public ExecutionEngineId Id
        {
            get
            {
                return _id;
            }
        }

        public IExecutionToken Execute(ITask task)
        {
            _additionalTasks.Add(task);
            return new ExecutionToken(task, this);
        }

        public bool IsActive()
        {
            return (_state == SuspendableState.Active);
        }

        public void Resume()
        {
            _state = SuspendableState.Active;
        }

        public void Suspend()
        {
            _state = SuspendableState.Suspended;
        }

        public void Update(TimeSpan delta)
        {
            if (_additionalTasks.Count > 0)
            {
                _tasks.AddRange(_additionalTasks);
            }

            if (IsActive())
            {
                foreach (ITask current in _tasks)
                {
                    if (current.State == TaskState.None)
                    {
                        current.Start();
                    }

                    if (current.State.IsActive())
                    {
                        current.Update(delta);
                    }

                    if (current.State == TaskState.Done)
                    {
                        current.Stop();
                        current.Dispose();
                    }
                }

                _tasks.RemoveAll(t => t.State == TaskState.Disposed);
            }
        }

        private bool TryCleanupTask(ITask task, List<ITask> container)
        {
            bool result = container.Contains(task);

            if (result)
            {
                if (task.State.IsActive())
                {
                    task.Stop();
                }

                if (task.State != TaskState.Disposed)
                {
                    task.Dispose();
                }
            }

            return result;
        }

		public void Dispose()
		{
			foreach (ITask task in _additionalTasks)
			{
				task.Dispose();
			}

			foreach (ITask task in _tasks)
			{
				task.Dispose();
			}

			_additionalTasks.Clear();
			_tasks.Clear();
		}
	}
}
