using Swisstalk.Foundation.Metadata;
using System;

namespace Swisstalk.Foundation.Tasks
{
    public interface ITask : IActiveObject, ILifecycle, IDisposable
    {
        TaskState State { get; }
    }
}
