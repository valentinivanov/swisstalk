using Swisstalk.Foundation.Tasks;

namespace Swisstalk.Core.Blocks.MetaScript
{
    public interface IMetaScriptEngineConfigurator
	{
		void RegisterChannel(IExecutionEngine channel);
		void UnregisterChannel(IExecutionEngine channel);
		void UnregisterChannel(ExecutionEngineId channelId);
	}
}
