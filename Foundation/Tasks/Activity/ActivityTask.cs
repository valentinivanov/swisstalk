using System;

namespace Swisstalk.Foundation.Tasks.Activity
{
    public class ActivityTask : ITask
    {
        private readonly IActivity _worker;
        private TaskState _taskState;

        public ActivityTask(IActivity worker)
        {
            _worker = worker;
            _taskState = TaskState.Undefined;
        }

        public TaskState State
        {
            get
            {
                return _taskState;
            }
        }

        public void Dispose()
        {
            _worker.Dispose();
            _taskState = TaskState.Disposed;
        }

        public void Start()
        {
            _worker.Start();
            _taskState = TaskState.Started;
        }

        public void Stop()
        {
            _worker.Stop();
            _taskState = TaskState.Stopped;
        }

        public void Update(TimeSpan delta)
        {
            if (_taskState == TaskState.Started)
            {
                _taskState = TaskState.Running;
            }

            if (_taskState == TaskState.Running && _worker.Update(delta))
            {
                _taskState = TaskState.Done;
            }
        }
    }
}
