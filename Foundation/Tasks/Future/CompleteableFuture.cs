using Swisstalk.Foundation.Metadata;
using Swisstalk.Foundation.Tasks.Token;

namespace Swisstalk.Foundation.Tasks.Future
{
    /// <summary>
    /// Allows to represent external long running operation.
    /// After operation result is ready, it should be set to the future via Complete() method.
    /// The future becomes defined after CompleteWith() call.
    /// </summary>
    /// <typeparam name="T">Type of data stored in the future.</typeparam>
    public class CompleteableFuture<T> : IFuture<T>, ICompleteable<T>
    {
        private T _instance;
        private readonly ICompleteableExecutionToken _token;

        public CompleteableFuture()
        {
            _instance = default(T);
            _token = new CompleteableExecutionToken();
        }

        public CompleteableFuture(ICompleteableExecutionToken token)
        {
            _instance = default(T);
            _token = token;
        }

        public void CompleteWith(T instance)
        {
            _instance = instance;
            _token.CompleteWith(TokenState.Done);
        }

        public T Instance
        {
            get
            {
                if (_token.State.IsDone())
                {
                    return _instance;
                }
                else
                {
                    throw new UndefinedFutureException();
                }
            }
        }

        public IExecutionToken Token
        {
            get
            {
                return _token;
            }
        }
    }
}
