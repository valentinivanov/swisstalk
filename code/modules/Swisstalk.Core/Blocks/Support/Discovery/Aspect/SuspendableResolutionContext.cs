using Swisstalk.Foundation.Behaviors.ActiveObject;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Core.Blocks.Support.Discovery;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery.Aspect
{
	public class SuspendableResolutionContext : IBlockResolutionContext
	{
		private readonly IBlockResolutionContext _decoratedContext;
		private readonly CompositeSuspendableObject _compositeObject;

		public SuspendableResolutionContext(IBlockResolutionContext decoratedContext)
		{
			_decoratedContext = decoratedContext;
			_compositeObject = new CompositeSuspendableObject();
		}

		public ISuspendable CompositeObject
		{
			get
			{
				return _compositeObject;	
			}
		}

		public ResolutionContextState ContextState
		{
			get
			{
				return _decoratedContext.ContextState;
			}
		}

		public void Activate()
		{
			_decoratedContext.Activate();
		}

		public void Append(IBlock item)
		{
			_decoratedContext.Append(item);

			if (item is ISuspendable)
			{
				_compositeObject.Append((ISuspendable)item);
			}
		}

		public void Deactivate()
		{
			_decoratedContext.Deactivate();
		}

		public void Remove(IBlock item)
		{
			_decoratedContext.Remove(item);

			if (item is ISuspendable)
			{
				_compositeObject.Remove((ISuspendable)item);
			}
		}

		public IBlock Resolve(BlockId serviceId)
		{
			return _decoratedContext.Resolve(serviceId);
		}
	}
}
