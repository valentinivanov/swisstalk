namespace Swisstalk.Foundation.Tasks
{
    public enum TaskState
    {
        Undefined = 0,
        Started,
        Running,
        Done,
        Stopped,
        Disposed
    }

    public static class TaskStateExtension
    {
        public static bool IsActive(this TaskState state)
        {
            return (state == TaskState.Running || state == TaskState.Started);
        }

        public static bool IsComplete(this TaskState state)
        {
            return (state == TaskState.Disposed);
        }

        public static TaskState GetMaxStateValue()
        {
            return TaskState.Disposed;
        }
    }
}
