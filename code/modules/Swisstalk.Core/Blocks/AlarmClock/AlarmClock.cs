using System;
using Swisstalk.Core.Blocks.Execution;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Tracking.Task;
using Swisstalk.Foundation.Tasks.Worker;
using Swisstalk.Core.Blocks.Time;

namespace Swisstalk.Core.Blocks.Timer
{
	public class AlarmClock : IBlock, IAlarmClock
    {
		private readonly BlockId _instanceId;
		private readonly IExecutor _executor;

		public AlarmClock(BlockId instanceId, IExecutor executor)
		{
			_instanceId = instanceId;
			_executor = executor;
		}

		public BlockId InstanceId
		{
			get
			{
				return _instanceId;
			}
		}

        public IExecutionToken SetAlarm(DateTime startTime, TimeSpan waitDuration, ISingleArgStatement<StateTrackingContext> alarmStatement)
        {
            ITask timerTask = new WorkerTask(new AlarmTaskWorker(startTime, waitDuration));
            return _executor.Execute(new StateTrackingTask(timerTask, TaskState.Disposed, alarmStatement));
        }

        public IExecutionToken SetAlarm(TimeSpan waitDuration, ISingleArgStatement<StateTrackingContext> alarmStatement)
        {
            ITask timerTask = new WorkerTask(new AlarmTaskWorker(FrameTime.Now, waitDuration));
			return _executor.Execute(new StateTrackingTask(timerTask, TaskState.Disposed, alarmStatement));
        }
    }
}
