using Swisstalk.Foundation.Behaviors.ActiveObject;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Core.Blocks.Support.Discovery;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery.Aspect
{
	public class ActiveObjectResolutionContext : IBlockResolutionContext
	{
		private readonly IBlockResolutionContext _decoratedContext;
		private readonly CompositeActiveObject _compositeObject;
		
		public ActiveObjectResolutionContext(IBlockResolutionContext decoratedContext)
		{
			_decoratedContext = decoratedContext;
			_compositeObject = new CompositeActiveObject();
		}

		public IActiveObject CompositeObject
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

			if (item is IActiveObject)
			{
				_compositeObject.Append((IActiveObject)item);
			}
		}

		public void Deactivate()
		{
			_decoratedContext.Deactivate();
		}

		public void Remove(IBlock item)
		{
			_decoratedContext.Remove(item);

			if (item is IActiveObject)
			{
				_compositeObject.Remove((IActiveObject)item);
			}
		}

		public IBlock Resolve(BlockId serviceId)
		{
			return _decoratedContext.Resolve(serviceId);
		}
	}
}
