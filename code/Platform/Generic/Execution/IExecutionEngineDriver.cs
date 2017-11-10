namespace Swisstalk.Platform.Generic.Execution
{
    public interface IExecutionEngineDriver
    {
        void UpdateFixedStep();
        void UpdatePreRender();
        void UpdatePostRender();
    }
}
