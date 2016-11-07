using System;
using Swisstalk.Foundation.Tasks.Worker;
using Swisstalk.Core.Blocks.Time;

namespace Swisstalk.Core.Blocks.Timer
{
    public class AlarmTaskWorker : ITaskWorker
	{
        private readonly DateTime _endTime;

		public AlarmTaskWorker(DateTime startTime, TimeSpan duration)
		{
            _endTime = startTime + duration;
		}

		public void Dispose()
		{
		}

		public void Start()
		{
		}

		public void Stop()
		{
		}

		public bool Update(TimeSpan delta)
		{
            return (FrameTime.Now >= _endTime);
        }
	}
}
