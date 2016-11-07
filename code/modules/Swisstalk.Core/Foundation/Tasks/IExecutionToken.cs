using System;

namespace Swisstalk.Foundation.Tasks
{
    public interface IExecutionToken : IDisposable
    {
        TaskState State { get; }
    }
}

