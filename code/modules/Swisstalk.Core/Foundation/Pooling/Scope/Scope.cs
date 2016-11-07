using System.Collections.Generic;

namespace Swisstalk.Foundation.Pooling.Scope
{
    public class Scope<T>
        where T : IScopeFrame
    {
        private Stack<T> _scopes;
        private T _head;

        public Scope()
        {
            _scopes = new Stack<T>();
        }

        public void PushFrame(T scope)
        {
            _scopes.Push(scope);
            _head = scope;
        }

        public void PopFrame()
        {
            IScopeFrame scope = _scopes.Pop();
            scope.Dispose();

            _head = (_scopes.Count > 0) ? _scopes.Peek() : default(T);
        }

        public T Current()
        {
            return _head;
        }

        public bool IsEmpty()
        {
            return (_scopes.Count == 0);
        }
    }
}
