using System;
using Swisstalk.Foundation.Composition;

namespace Swisstalk.Foundation.Pooling.Scope.Disposable
{
    public class DisposableScopeFrame : UniqueComposition<IDisposable>, IScopeFrame
    {
        public void Dispose()
        {
            foreach (IDisposable item in Composite)
            {
                item.Dispose();
            }
        }
    }
}
