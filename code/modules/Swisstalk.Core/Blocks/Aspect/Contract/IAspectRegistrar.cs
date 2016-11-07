using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Core.Blocks.Aspect
{
	public interface IAspectRegistrar
	{
		void Register<T>(T aspect) where T : IAspect;
		void Unregister<T>(string aspectId) where T : IAspect;
	}
}
