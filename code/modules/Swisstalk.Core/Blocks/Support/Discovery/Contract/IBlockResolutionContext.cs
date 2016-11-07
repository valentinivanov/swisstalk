using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery
{
	public enum ResolutionContextState
	{
		None,
		Active,
		Inactive
	}
	
	public interface IBlockResolutionContext : IAppendable<IBlock>, IRemoveable<IBlock>
	{
		IBlock Resolve(BlockId serviceId);

		void Activate();
		void Deactivate();

		ResolutionContextState ContextState
		{
			get;
		}
	}
}
