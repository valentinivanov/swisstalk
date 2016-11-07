namespace Swisstalk.Foundation.Tasks.Tracking.Task
{
    public struct StateTrackingContext
    {
        private readonly ITask _task;
        private readonly TaskState _firingState;
        private readonly TaskState _fromState;

        public StateTrackingContext(ITask task,
                                 TaskState firingState,
                                 TaskState fromState)
        {
            _task = task;
            _firingState = firingState;
            _fromState = fromState;
        }

        public ITask Task
        {
            get
            {
                return _task;
            }
        }

        public TaskState FiringState
        {
            get
            {
                return _firingState;
            }
        }

        public TaskState FromState
        {
            get
            {
                return _fromState;
            }
        }
    }
}
