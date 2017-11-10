using Swisstalk.Foundation.Tasks;

namespace Swisstalk.Platform.Generic.Execution
{
    public interface IExecutionEngineRepository
    {
        void AllocateParallelWithFixedStepUpdate(int engineId);
        void AllocateParallelWithPreRenderUpdate(int engineId);
        void AllocateParallelWithPostRenderUpdate(int engineId);

        void AllocateSequentialWithFixedStepUpdate(int engineId);
        void AllocateSequentialWithPreRenderUpdate(int engineId);
        void AllocateSequentialWithPostRenderUpdate(int engineId);

        void Clear();

        IExecutionEngine Resolve(int engineId);
    }
}
