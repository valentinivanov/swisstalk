using Swisstalk.Foundation.Metadata;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Activity;

namespace Swisstalk.Platform.Generic.Input
{
    public class InputManager : ILifecycle
    {
        private readonly IExecutor engine;
        private readonly IInputActivityBuilder inputActivityBuilder;

        private IExecutionToken inputDetectionToken;

        public InputManager(IExecutor engine,
                            IInputActivityBuilder inputActivityBuilder)
        {
            this.engine = engine;
            this.inputActivityBuilder = inputActivityBuilder;
        }

        public void Start()
        {
            IActivity inputDetectionActivities = inputActivityBuilder.Build();
            inputDetectionToken = engine.Execute(inputDetectionActivities);
        }

        public void Stop()
        {
            if (inputDetectionToken != null)
            {
                inputDetectionToken.Cancel();
                inputDetectionToken = null;
            }
        }
    }
}
