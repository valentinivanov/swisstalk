using System;

namespace Swisstalk.Platform.Generic.FrameTime
{
    public class FrameTimeProvider : IFrameTimeProvider
    {
        private struct FrameTime
        {
            public DateTime Time;
            public TimeSpan DeltaTime;

            public void Update()
            {
                DateTime current = DateTime.Now;

                DeltaTime = current - Time;
                Time = current;
            }
        }

        private FrameTime fixedFrameTime;
        private FrameTime preRenderFrameTime;

        public FrameTimeProvider()
        {
            FixedStepUpdate();
            PreRenderUpdate();
        }

        public DateTime PreRenderTime
        {
            get
            {
                return preRenderFrameTime.Time;
            }
        }

        public DateTime FixedStepTime
        {
            get
            {
                return fixedFrameTime.Time;
            }
        }

        public TimeSpan PreRenderDelta
        {
            get
            {
                return preRenderFrameTime.DeltaTime;
            }
        }

        public TimeSpan FixedStepDelta
        {
            get
            {
                return fixedFrameTime.DeltaTime;
            }
        }

        public void FixedStepUpdate()
        {
            fixedFrameTime.Update();
        }

        public void PreRenderUpdate()
        {
            preRenderFrameTime.Update();
        }
    }
}
