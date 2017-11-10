using Swisstalk.Foundation.Behaviors.ActiveObject;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Engine;
using Swisstalk.Platform.Generic.FrameTime;
using System.Collections.Generic;

namespace Swisstalk.Platform.Generic.Execution
{
    public class ExecutionEngineService : IExecutionEngineDriver,
                                          IExecutionEngineRepository
    {
        private readonly IFrameTimeProvider frameTime;

        private readonly CompositeActiveObject fixedStepEngines;
        private readonly CompositeActiveObject preRenderEngines;
        private readonly CompositeActiveObject postRenderEngines;

        private readonly Dictionary<int, IExecutionEngine> engineLookupTable;

        public ExecutionEngineService(IFrameTimeProvider frameTime)
        {
            this.frameTime = frameTime;

            fixedStepEngines = new CompositeActiveObject();
            preRenderEngines = new CompositeActiveObject();
            postRenderEngines = new CompositeActiveObject();

            engineLookupTable = new Dictionary<int, IExecutionEngine>();
        }

        public void AllocateParallelWithFixedStepUpdate(int engineId)
        {
            RegisterEngine(new ParallelExecutionEngine(), fixedStepEngines, engineId);
        }

        public void AllocateParallelWithPostRenderUpdate(int engineId)
        {
            RegisterEngine(new ParallelExecutionEngine(), postRenderEngines, engineId);
        }

        public void AllocateParallelWithPreRenderUpdate(int engineId)
        {
            RegisterEngine(new ParallelExecutionEngine(), preRenderEngines, engineId);
        }

        public void AllocateSequentialWithFixedStepUpdate(int engineId)
        {
            RegisterEngine(new SequentialExecutionEngine(), fixedStepEngines, engineId);
        }

        public void AllocateSequentialWithPostRenderUpdate(int engineId)
        {
            RegisterEngine(new SequentialExecutionEngine(), postRenderEngines, engineId);
        }

        public void AllocateSequentialWithPreRenderUpdate(int engineId)
        {
            RegisterEngine(new SequentialExecutionEngine(), preRenderEngines, engineId);
        }

        public void Clear()
        {
            engineLookupTable.Clear();
            fixedStepEngines.Clear();
            preRenderEngines.Clear();
            postRenderEngines.Clear();
        }

        public IExecutionEngine Resolve(int engineId)
        {
            return engineLookupTable[engineId];
        }

        public void UpdateFixedStep()
        {
            fixedStepEngines.Update(frameTime.FixedStepDelta);
        }

        public void UpdatePostRender()
        {
            postRenderEngines.Update(frameTime.PreRenderDelta);
        }

        public void UpdatePreRender()
        {
            preRenderEngines.Update(frameTime.PreRenderDelta);
        }

        private void RegisterEngine(IExecutionEngine engine, CompositeActiveObject updater, int engineId)
        {
            engineLookupTable.Add(engineId, engine);
            updater.Append(engine);
        }
    }
}
