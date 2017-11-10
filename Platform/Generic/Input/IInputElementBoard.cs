using Swisstalk.Platform.Generic.Input.Elements;

namespace Swisstalk.Platform.Generic.Input
{
    public interface IInputElementBoard
    {
        IAxis1D<float> GetFloatAxis1D(int axisId);
        IAxis1D<int> GetIntegerAxis1D(int axisId);
        IButton GetButton(int buttonId);
    }
}
