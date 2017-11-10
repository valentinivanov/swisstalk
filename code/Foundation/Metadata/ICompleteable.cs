namespace Swisstalk.Foundation.Metadata
{
    public interface ICompleteable<T>
    {
        void CompleteWith(T state);
    }
}
