using Swisstalk.Platform.Generic.Input.Elements;
using System.Collections.Generic;

namespace Swisstalk.Platform.Generic.Input
{
    public class InputElementBoard : IInputElementBoard
    {
        private readonly Dictionary<int, IAxis1D<float>> floatAxises = new Dictionary<int, IAxis1D<float>>();
        private readonly Dictionary<int, IAxis1D<int>> integerAxises = new Dictionary<int, IAxis1D<int>>();
        private readonly Dictionary<int, IButton> buttons = new Dictionary<int, IButton>();

        public void AddFloatAxis1D(IAxis1D<float> axis)
        {
            floatAxises.Add(axis.Id, axis);
        }

        public void AddIntegerAxis1D(IAxis1D<int> axis)
        {
            integerAxises.Add(axis.Id, axis);
        }

        public void AddButton(IButton button)
        {
            buttons.Add(button.Id, button);
        }

        public IAxis1D<float> GetFloatAxis1D(int axisId)
        {
            return floatAxises[axisId];
        }

        public IAxis1D<int> GetIntegerAxis1D(int axisId)
        {
            return integerAxises[axisId];
        }

        public IButton GetButton(int buttonId)
        {
            return buttons[buttonId];
        }
    }
}
