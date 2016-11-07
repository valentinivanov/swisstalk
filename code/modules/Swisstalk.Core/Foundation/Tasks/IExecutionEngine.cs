using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Tasks
{
    public interface IExecutionEngine : IActiveObject, ISuspendable
    {
        ExecutionEngineId Id { get; }
        IExecutionToken Execute(ITask task);
    }
}
