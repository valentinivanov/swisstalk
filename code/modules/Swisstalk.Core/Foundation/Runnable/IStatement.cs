namespace Swisstalk.Foundation.Runnable
{
    public interface IVoidArgStatement
    {
        void Execute();
    }

    public interface ISingleArgStatement<ArgType>
    {
        void Execute(ArgType obj);
    }

    public interface IDoubleArgStatement<ArgType0, ArgType1>
    {
        void Execute(ArgType0 param0, ArgType1 param1);
    }

    public interface IResultStatement<ReturnType>
    {
        ReturnType Result { get; }
    }

    public interface IReturnStatement<ReturnType>
    {
        ReturnType Execute();
    }
}
