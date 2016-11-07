using Swisstalk.Foundation.Behaviors.ActiveObject;
using Swisstalk.Core.Blocks.Support.Discovery;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using System;

namespace Swisstalk.Core.Blocks.Support.Discovery.Aspect
{
	public class DisposableResolutionContext : IBlockResolutionContext
	{
		private readonly IBlockResolutionContext _decoratedContext;
		private readonly CompositeDisposableObject _compositeObject;

		public DisposableResolutionContext(IBlockResolutionContext decoratedContext)
		{
			_decoratedContext = decoratedContext;
			_compositeObject = new CompositeDisposableObject();
		}

		public IDisposable CompositeObject
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

			if (item is IDisposable)
			{
				_compositeObject.Append((IDisposable)item);
			}
		}

		public void Deactivate()
		{
			_decoratedContext.Deactivate();
		}

		public void Remove(IBlock item)
		{
			_decoratedContext.Remove(item);

			if (item is IDisposable)
			{
				_compositeObject.Remove((IDisposable)item);
			}
		}

		public IBlock Resolve(BlockId serviceId)
		{
			return _decoratedContext.Resolve(serviceId);
		}
	}
}
