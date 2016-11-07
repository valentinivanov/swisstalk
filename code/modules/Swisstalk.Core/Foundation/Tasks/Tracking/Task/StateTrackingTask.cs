using System;
using Swisstalk.Foundation.Runnable;

namespace Swisstalk.Foundation.Tasks.Tracking.Task
{
    public class StateTrackingTask : ITask
    {
        private readonly ITask _trackingTask;
        private readonly TaskState _trackingState;
        private readonly ISingleArgStatement<StateTrackingContext> _callbackStatement;

        public StateTrackingTask(ITask trackingTask,
                                 TaskState trackingState,
                                 ISingleArgStatement<StateTrackingContext> callbackStatement)
        {
            _trackingTask = trackingTask;
            
            _trackingState = trackingState;
            _callbackStatement = callbackStatement;
        }

        public TaskState State
        {
            get
            {
                return _trackingTask.State;
            }
        }

        public void Dispose()
        {
            TaskState prevState = _trackingTask.State;
            _trackingTask.Dispose();

            EvaluateState(_trackingTask.State, prevState);
        }

        public void Start()
        {
            TaskState prevState = _trackingTask.State;
            _trackingTask.Start();

            EvaluateState(_trackingTask.State, prevState);
        }

        public void Stop()
        {
            TaskState prevState = _trackingTask.State;
            _trackingTask.Stop();

            EvaluateState(_trackingTask.State, prevState);
        }

        public void Update(TimeSpan delta)
        {
            TaskState prevState = _trackingTask.State;
            _trackingTask.Update(delta);

            EvaluateState(_trackingTask.State, prevState);
        }

        private void EvaluateState(TaskState currentState, TaskState prevState)
        {
            if (currentState == _trackingState)
            {
                StateTrackingContext args = new StateTrackingContext(_trackingTask, currentState, prevState);
                _callbackStatement.Execute(args);
            }
        }
    }
}
