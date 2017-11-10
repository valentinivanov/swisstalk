using Swisstalk.Foundation.Behaviors;
using Swisstalk.Foundation.Composition;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Tasks.Token
{
    public class CompositeExecutionToken : CompositeObject<IExecutionToken>, 
                                           IExecutionToken,
                                           IDisposable
    {
        public CompositeExecutionToken() : base()
        {
        }

        public CompositeExecutionToken(params IExecutionToken[] tokens) : base(tokens)
        {
        }

        public CompositeExecutionToken(IEnumerable<IExecutionToken> tokens) : base(tokens)
        {
        }

        public TokenState State
        {
            get
            {
                return GetEffectiveTaskState();
            }
        }

        public bool IsAnyCompleted ()
        {
            foreach (IExecutionToken item in Composite)
            {
                if (item.State.IsComplete())
                {
                    return true;
                }
            }

            return false;
        }

        public void Cancel()
        {
            foreach (IExecutionToken item in Composite)
            {
                if (item.State.IsActive())
                {
                    item.Cancel();
                }
            }
        }

        public void Dispose()
        {
            foreach (IExecutionToken item in Composite)
            {
                item.TryDispose();
            }
        }

        private TokenState GetEffectiveTaskState()
        {
            TokenState effectiveState = TokenStateExtension.GetMaxStateValue();

            foreach (IExecutionToken item in Composite)
            {
                if (item.State < effectiveState)
                {
                    effectiveState = item.State;
                }
            }

            return effectiveState;
        }
    }
}
