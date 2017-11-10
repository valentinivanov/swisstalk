using Swisstalk.Foundation.Behaviors;

namespace Swisstalk.Platform.Generic.Input.Elements
{
    public class Button : IButton
    {
        private readonly int buttonId;
        private readonly WeakEvent<ButtonBeginPressing> beginPressingEvent = new WeakEvent<ButtonBeginPressing>();
        private readonly WeakEvent<ButtonPressing> pressingEvent = new WeakEvent<ButtonPressing>();
        private readonly WeakEvent<ButtonEndPressing> endPressingEvent = new WeakEvent<ButtonEndPressing>();

        public Button(int buttonId)
        {
            this.buttonId = buttonId;
        }

        public int Id
        {
            get
            {
                return buttonId;
            }
        }

        public void InvokeBeginPressing()
        {
            beginPressingEvent.Invoke(d => d(this));
        }

        public void InvokeEndPressing()
        {
            endPressingEvent.Invoke(d => d(this));
        }

        public void InvokePressing()
        {
            pressingEvent.Invoke(d => d(this));
        }

        public IWeakEvent<ButtonBeginPressing> BeginPressing
        {
            get
            {
                return beginPressingEvent;
            }
        }

        public IWeakEvent<ButtonEndPressing> EndPressing
        {
            get
            {
                return endPressingEvent;
            }
        }

        public IWeakEvent<ButtonPressing> Pressing
        {
            get
            {
                return pressingEvent;
            }
        }
    }
}
