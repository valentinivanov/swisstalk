namespace Swisstalk.Foundation.Pooling.Scope.Disposable
{
    public static class DisposableScope
    {
        public static Scope<DisposableScopeFrame> New()
        {
            Scope<DisposableScopeFrame> scope = new Scope<DisposableScopeFrame>();
            scope.PushFrame(new DisposableScopeFrame());

            return scope;
        }

        public static Scope<DisposableScopeFrame> PushFrame(this Scope<DisposableScopeFrame> self)
        {
            self.PushFrame(new DisposableScopeFrame());
            return self;
        }
    }
}
