using System;

namespace Swisstalk.Foundation.Tasks.Worker
{
    public class WorkerTask : ITask
    {
        private readonly ITaskWorker _worker;
        private TaskState _taskState;

        public WorkerTask(ITaskWorker worker)
        {
            _worker = worker;
            _taskState = TaskState.None;
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
