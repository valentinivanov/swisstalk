using Swisstalk.Foundation.Tasks.Activity;
using Swisstalk.Platform.Generic.Input.Elements;
using System;

namespace Swisstalk.Platform.Unity3d.Input.Activities
{
    public class MouseXToFloatAxis1DActivity : IActivity
    {
        private readonly FloatAxis1D axis;

        public MouseXToFloatAxis1DActivity(FloatAxis1D axis)
        {
            this.axis = axis;
        }

        public void Start()
        {
        }

        public bool Update(TimeSpan delta)
        {
            float t = UnityEngine.Input.mousePosition.x / UnityEngine.Screen.width;

            float value = UnityEngine.Mathf.Lerp(axis.MinValue, axis.MaxValue, t);

            axis.SetValue(value);

            return false;
        }

        public void Stop()
        {
        }

        public void Dispose()
        {
        }
    }
}
