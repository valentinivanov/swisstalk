using Swisstalk.Foundation.Tasks.Activity;
using Swisstalk.Platform.Generic.FrameTime;
using System;

namespace Swisstalk.Services
{
    public class TimerActivity : IActivity
	{
        private readonly FrameTimeProvider timeProvider;
        private readonly DateTime endTime;
        private readonly object cookie;
        private readonly TimerCompletionNotification completionAction;

        private bool completedViaUpdate;

        public TimerActivity(object cookie,
                             TimerCompletionNotification completionAction,
                             FrameTimeProvider timeProvider,
                             DateTime startTime, 
                             TimeSpan duration)
		{
            this.cookie = cookie;
            this.completionAction = completionAction;
            this.timeProvider = timeProvider;
            endTime = startTime + duration;

            completedViaUpdate = false;
        }

		public void Dispose()
		{
		}

		public void Start()
		{
            completedViaUpdate = false;
        }

		public void Stop()
		{
            if (completionAction != null)
            {
                completionAction(cookie, !completedViaUpdate);
            }
		}

		public bool Update(TimeSpan delta)
		{
            completedViaUpdate = (timeProvider.PreRenderTime >= endTime);
            return completedViaUpdate;
        }
	}
}
