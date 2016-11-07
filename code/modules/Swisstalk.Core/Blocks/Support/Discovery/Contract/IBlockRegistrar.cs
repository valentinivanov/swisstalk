using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Core.Blocks.Support.Discovery
{
	public interface IBlockRegistrar : IAppendable<IBlockResolutionContext>, IRemoveable<IBlockResolutionContext>
	{		
	}
}
