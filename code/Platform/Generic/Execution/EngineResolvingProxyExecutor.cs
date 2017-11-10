using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Activity;
using System;

namespace Swisstalk.Platform.Generic.Execution
{
    public class EngineResolvingProxyExecutor : IExecutor
    {
        private readonly int engineId;
        private readonly IExecutionEngineRepository engineRepository;

        private IExecutionEngine engine;

        private Func<IExecutionEngine> engineGetter;

        public EngineResolvingProxyExecutor(int engineId, IExecutionEngineRepository engineRepository)
        {
            this.engineId = engineId;
            this.engineRepository = engineRepository;
            this.engineGetter = GetEngineWithResolveAndStore;
        }

        public IExecutionToken Execute(IActivity activity)
        {
            return engineGetter().Execute(activity);
        }

        public IExecutionToken Execute(ITask task)
        {
            return engineGetter().Execute(task);
        }

        private IExecutionEngine GetEngineWithResolveAndStore()
        {
            engine = engineRepository.Resolve(engineId);

            if (engine != null)
            {
                engineGetter = GetEngineDirect;
            }

            return engine;
        }

        private IExecutionEngine GetEngineDirect()
        {
            return engine;
        }
    }
}
