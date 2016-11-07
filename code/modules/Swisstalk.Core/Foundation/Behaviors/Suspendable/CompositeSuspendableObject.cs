using Swisstalk.Foundation.Composition;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Behaviors.ActiveObject
{
	public class CompositeSuspendableObject : UniqueReversibleComposition<ISuspendable>, ISuspendable
	{
		public bool IsActive()
		{
			foreach (ISuspendable suspendable in Composite)
			{
				if (suspendable.IsActive())
				{
					return true;
				}
			}

			return false;
		}

		public void Resume()
		{
			foreach (ISuspendable suspendable in Composite)
			{
				suspendable.Resume();
			}
		}

		public void Suspend()
		{
			foreach (ISuspendable suspendable in Composite)
			{
				suspendable.Suspend();
			}
		}
	}
}
