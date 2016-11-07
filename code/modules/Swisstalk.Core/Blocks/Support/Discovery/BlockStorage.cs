using System.Collections.Generic;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery
{
	public class BlockStorage : IBlockLocator, IBlockRegistrar
	{
		private readonly List<IBlockResolutionContext> _resolutionStack;
		
		public BlockStorage()
		{
			_resolutionStack = new List<IBlockResolutionContext>();
		}

		public void Append(IBlockResolutionContext item)
		{
			_resolutionStack.Add(item);
			item.Activate();
		}

		public void Remove(IBlockResolutionContext item)
		{
			item.Deactivate();
			_resolutionStack.Remove(item);
		}

		public T Resolve<T>(BlockId serviceId)
		{
			for (int i = _resolutionStack.Count - 1; i >= 0; i--)
			{
				IBlock service = _resolutionStack[i].Resolve(serviceId);
				if (service != null)
				{
					return (T)service;
				}
			}

			throw new BlockNotResolvedException();
		}
	}
}
