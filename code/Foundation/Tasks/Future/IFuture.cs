namespace Swisstalk.Foundation.Tasks.Future
{
    public interface IFuture
    {
        IExecutionToken Token
        {
            get;
        }
    }

    public interface IFuture<T> : IFuture
    {
        /// <summary>
        /// Throws an UndefinedFutureException if instance is requested on a pending token
        /// </summary>
        T Instance
        {
            get;
        }     
    }
}
