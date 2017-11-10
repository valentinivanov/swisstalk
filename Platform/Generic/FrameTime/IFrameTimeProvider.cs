using System;

namespace Swisstalk.Platform.Generic.FrameTime
{
    public interface IFrameTimeProvider
    {
        DateTime PreRenderTime { get; }
        TimeSpan PreRenderDelta { get; }

        DateTime FixedStepTime { get; }
        TimeSpan FixedStepDelta { get; }
    }
}
