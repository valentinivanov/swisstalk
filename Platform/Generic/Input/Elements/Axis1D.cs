using Swisstalk.Foundation.Behaviors;

namespace Swisstalk.Platform.Generic.Input.Elements
{
    public abstract class Axis1D<T> : IAxis1D<T>
    {
        private readonly WeakEvent<Axis1DValueChanged<T>> valueChangeEvent = new WeakEvent<Axis1DValueChanged<T>>();
        private readonly T minValue;
        private readonly T maxValue;
        private readonly int axisId;

        private T value;

        public Axis1D(int axisId, T minValue, T maxValue)
        {
            this.axisId = axisId;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = Clamp(default(T));
        }

        public void SetValue(T newValue)
        {
            T currentValue = value;
            value = Clamp(newValue);

            if (!currentValue.Equals(newValue))
            {
                valueChangeEvent.Invoke(d => d(this, currentValue, newValue));
            }
        }

        public int Id
        {
            get
            {
                return axisId;
            }
        }

        public T Value
        {
            get
            {
                return value;
            }
        }

        public IWeakEvent<Axis1DValueChanged<T>> ValueChanged
        {
            get
            {
                return valueChangeEvent;
            }
        }

        public T MinValue
        {
            get
            {
                return minValue;
            }
        }

        public T MaxValue
        {
            get
            {
                return maxValue;
            }
        }

        protected abstract T Clamp(T newValue);
    }
}
