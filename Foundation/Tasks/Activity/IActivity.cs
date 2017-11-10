using Swisstalk.Foundation.Metadata;
using System;

namespace Swisstalk.Foundation.Tasks.Activity
{
    public interface IActivity : ILifecycle, IDisposable
    {
        bool Update(TimeSpan delta);
    }
}
