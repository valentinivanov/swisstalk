using System;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Tasks.Worker
{
    public interface ITaskWorker : ILifecycle, IDisposable
    {
        bool Update(TimeSpan delta);
    }
}
