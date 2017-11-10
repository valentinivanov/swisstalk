using Swisstalk.Foundation.Behaviors;

namespace Swisstalk.Platform.Generic.Input.Elements
{
    public delegate void ButtonBeginPressing(IButton button);
    public delegate void ButtonEndPressing(IButton button);
    public delegate void ButtonPressing(IButton button);

    public interface IButton
    {
        int Id
        {
            get;
        }

        IWeakEvent<ButtonBeginPressing> BeginPressing
        {
            get;
        }

        IWeakEvent<ButtonEndPressing> EndPressing
        {
            get;
        }

        IWeakEvent<ButtonPressing> Pressing
        {
            get;
        }
    }
}
