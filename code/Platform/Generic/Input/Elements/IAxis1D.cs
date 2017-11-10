using Swisstalk.Foundation.Behaviors;

namespace Swisstalk.Platform.Generic.Input.Elements
{
    public delegate void Axis1DValueChanged<T>(IAxis1D<T> axis, T oldValue, T newValue);

    public interface IAxis1D<T>
    {
        int Id
        {
            get;
        }

        T Value
        {
            get;
        }

        T MinValue
        {
            get;
        }

        T MaxValue
        {
            get;
        }

        IWeakEvent<Axis1DValueChanged<T>> ValueChanged
        {
            get;
        }
    }
}
