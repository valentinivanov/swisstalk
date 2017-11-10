using Swisstalk.Platform.Generic.Input;
using Swisstalk.Platform.Generic.Input.Elements;

namespace Swisstalk.Platform.Generic.Input
{
    public class InputMethod : IInputElementBoard
    {
        private readonly InputElementBoard methodBoard;

        public InputMethod()
        {
            methodBoard = new InputElementBoard();
        }

        protected InputElementBoard MethodBoard
        {
            get
            {
                return methodBoard;
            }
        }

        public IButton GetButton(int buttonId)
        {
            return methodBoard.GetButton(buttonId);
        }

        public IAxis1D<float> GetFloatAxis1D(int axisId)
        {
            return methodBoard.GetFloatAxis1D(axisId);
        }

        public IAxis1D<int> GetIntegerAxis1D(int axisId)
        {
            return methodBoard.GetIntegerAxis1D(axisId);
        }
    }
}
