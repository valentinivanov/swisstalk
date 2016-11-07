using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Core.Blocks.Aspect
{
	public interface IAspectLocator
	{
		T Get<T>(string aspectId) where T : IAspect;
	}
}
