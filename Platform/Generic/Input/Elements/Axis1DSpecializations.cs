using System;

namespace Swisstalk.Platform.Generic.Input.Elements
{
    public class IntegerAxis1D : Axis1D<int>
    {
        public IntegerAxis1D(int axisId, int minValue, int maxValue) : base(axisId, minValue, maxValue)
        {
        }

        protected override int Clamp(int newValue)
        {
            return Math.Max(MinValue, Math.Min(newValue, MaxValue));
        }
    }

    public class FloatAxis1D : Axis1D<float>
    {
        public FloatAxis1D(int axisId, float minValue, float maxValue) : base(axisId, minValue, maxValue)
        {
        }

        protected override float Clamp(float newValue)
        {
            return Math.Max(MinValue, Math.Min(newValue, MaxValue));
        }
    }
}
