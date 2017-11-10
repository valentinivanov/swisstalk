using System;

namespace Swisstalk.Foundation.Behaviors
{
    public static class DisposableExtensions
    {
        public static void DisposeIfNotNull(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public static void TryDispose(this object obj)
        {
            IDisposable disposable = obj as IDisposable;
            disposable.DisposeIfNotNull();
        }
    }
}
