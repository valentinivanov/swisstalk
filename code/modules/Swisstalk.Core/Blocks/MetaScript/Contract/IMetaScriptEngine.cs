using Swisstalk.Foundation.Tasks;

namespace Swisstalk.Core.Blocks.MetaScript
{
    public interface IMetaScriptEngine
    {
		IExecutionEngine GetChannel(ExecutionEngineId channelId);
    }
}
