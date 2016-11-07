using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Tasks;

namespace Swisstalk.Core.Blocks.Execution
{
	public interface IExecutor : IBlock
	{
		IExecutionToken Execute(ITask task);
	}
}
