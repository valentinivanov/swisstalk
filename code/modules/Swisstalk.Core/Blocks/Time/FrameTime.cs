using System;

namespace Swisstalk.Core.Blocks.Time
{
    public static class FrameTime
    {
        private static DateTime _now;
        private static TimeSpan _frameDelta;
        private static TimeSpan _fixedDelta;

        public static DateTime Now
        {
            get
            {
                return _now;
            }

            set
            {
                _now = value;
            }
        }

        public static TimeSpan FrameDelta
        {
            get
            {
                return _frameDelta;
            }

            set
            {
                _frameDelta = value;
            }
        }

        public static TimeSpan FixedDelta
        {
            get
            {
                return _fixedDelta;
            }

            set
            {
                _fixedDelta = value;
            }
        }
    }
}

