using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery
{
    public interface IBlockLocator
    {
        T Resolve<T>(BlockId serviceId);
    }
}