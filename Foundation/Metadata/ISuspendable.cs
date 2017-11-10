namespace Swisstalk.Foundation.Metadata
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

