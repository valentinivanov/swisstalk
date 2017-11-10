using Swisstalk.Foundation.Metadata;
using Swisstalk.Foundation.Tasks.Activity;
using Swisstalk.Foundation.Tasks.Token;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Tasks.Engine
{
    public class ParallelExecutionEngine : IExecutionEngine, IRemovable<ITask>, IDisposable
    {
        private readonly List<ITask> _tasks;
        private readonly List<ITask> _additionalTasks;

        private SuspendableState _state;

        public ParallelExecutionEngine()
        {
            _state = SuspendableState.Active;

            _tasks = new List<ITask>();
            _additionalTasks = new List<ITask>();
        }

        bool IRemovable<ITask>.Remove(ITask item)
        {
            if (TryCleanupTask(item, _tasks))
            {
                return true;
            }
            else {
                if (TryCleanupTask(item, _additionalTasks))
                {
                    return _additionalTasks.Remove(item);
                }
                else
                {
                    return false;
                }
            }
        }

        public IExecutionToken Execute(ITask task)
        {
            _additionalTasks.Add(task);
            return new TaskExecutionToken(task, this);
        }

        public IExecutionToken Execute(IActivity activity)
        {
            return Execute(new ActivityTask(activity));
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
				_additionalTasks.Clear();
            }

            if (IsActive())
            {
                foreach (ITask current in _tasks)
                {
                    if (current.State == TaskState.Undefined)
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
