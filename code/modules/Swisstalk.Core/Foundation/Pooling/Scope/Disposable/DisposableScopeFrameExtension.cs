using System;

namespace Swisstalk.Foundation.Pooling.Scope.Disposable
{
    public static class DisposableScopeFrameExtension
    {
        public static T AutoScope<T>(this T self, Scope<DisposableScopeFrame> scope)
            where T : IDisposable
        {
            scope.Current().Append(self);

            return self;
        }
    }
}
