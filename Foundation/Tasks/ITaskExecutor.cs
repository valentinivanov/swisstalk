namespace Swisstalk.Foundation.Tasks
{
    public interface ITaskExecutor
    {
        IExecutionToken Execute(ITask task);
    }
}
