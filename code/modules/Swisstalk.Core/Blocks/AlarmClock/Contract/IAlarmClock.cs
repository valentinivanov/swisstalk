using System;
using Swisstalk.Foundation.Runnable;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Tracking.Task;

namespace Swisstalk.Core.Blocks.Timer
{
	public interface IAlarmClock
	{
		IExecutionToken SetAlarm(DateTime startTime, TimeSpan waitDuration, ISingleArgStatement<StateTrackingContext> alarmStatement);
		IExecutionToken SetAlarm(TimeSpan waitDuration, ISingleArgStatement<StateTrackingContext> alarmStatement);
	}
}
