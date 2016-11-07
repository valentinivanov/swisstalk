using System;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Tasks
{
    public interface ITask : IActiveObject, ILifecycle, IDisposable
    {
        TaskState State { get; }
    }
}
