using Swisstalk.Foundation.Tasks;
using Swisstalk.Platform.Generic.FrameTime;
using System;

namespace Swisstalk.Services
{
    public delegate void TimerCompletionNotification(object cookie, bool isInterrupted);

    public class Timer
    {
        private readonly IExecutor executor;
        private readonly FrameTimeProvider frameTime;
       
		public Timer(IExecutor executor,
                     FrameTimeProvider frameTime)
		{
            this.executor = executor;
            this.frameTime = frameTime;
        }

        public IExecutionToken SetTimer(TimeSpan waitDuration, TimerCompletionNotification completionAction, object cookie)
        {
            return executor.Execute(new TimerActivity(cookie, completionAction, frameTime, frameTime.PreRenderTime, waitDuration));
        }
    }
}
