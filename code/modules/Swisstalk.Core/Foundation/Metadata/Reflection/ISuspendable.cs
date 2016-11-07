using System;

namespace Swisstalk.Foundation.Metadata.Reflection
{    
	public enum SuspendableState
	{
		None,
		Active,
		Suspended
	}

    public interface ISuspendable
    {
        void Suspend();
        void Resume();
        bool IsActive();
    }
}

