using Swisstalk.Foundation.Tasks.Activity;
using Swisstalk.Platform.Generic.Input.Elements;
using System;
using UnityEngine;

namespace Swisstalk.Platform.Unity3d.Input.Activities
{
    public class KeyboardKeyToButtonActivity : IActivity
    {
        private enum KeyState
        {
            Idle,
            Pressed
        }

        private readonly KeyCode keyCode;
        private readonly Button mappedButton;

        private KeyState keyState;

        public KeyboardKeyToButtonActivity(KeyCode keyCode, Button mappedButton)
        {
            this.keyCode = keyCode;
            this.mappedButton = mappedButton;
            this.keyState = KeyState.Idle;
        }

        public void Start()
        {
        }

        public bool Update(TimeSpan delta)
        {
            KeyState nextState = UnityEngine.Input.GetKey(keyCode) ? KeyState.Pressed : KeyState.Idle;

            OnKeyStateTransition(keyState, nextState);

            keyState = nextState;

            return false;
        }

        public void Stop()
        {
            
        }

        public void Dispose()
        {
        }

        private void OnKeyStateTransition(KeyState from, KeyState to)
        {
            if (from == KeyState.Idle && to == KeyState.Pressed)
            {
                mappedButton.InvokeBeginPressing();
            }
            else if (from == KeyState.Pressed && to == KeyState.Pressed)
            {
                mappedButton.InvokePressing();
            }
            else if (from == KeyState.Pressed && to == KeyState.Idle)
            {
                mappedButton.InvokeEndPressing();
            }
        }
    }
}
