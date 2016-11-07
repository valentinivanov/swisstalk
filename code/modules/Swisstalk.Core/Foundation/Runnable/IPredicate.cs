namespace Swisstalk.Foundation.Runnable
{
    public interface IPredicate
    {
        bool Evaluate();
    }

    public interface IPredicate<T>
    {
        bool Evaluate(T v);
    }
}
