using System;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Engine;

namespace Swisstalk.Core.Blocks.Execution
{
	public class Executor : IExecutor, IDisposable, ISuspendable, IActiveObject
	{
		private readonly BlockId _instanceId;
		private readonly ParallelExecutionEngine _executionEngine;

		public Executor(BlockId instanceId)
		{
			_instanceId = instanceId;
			_executionEngine = new ParallelExecutionEngine(new ExecutionEngineId(instanceId.Description));
		}

		public BlockId InstanceId
		{
			get
			{
				return _instanceId;
			}
		}

		public bool IsActive()
		{
			return _executionEngine.IsActive();
		}

		public void Resume()
		{
			_executionEngine.Resume();
		}

		public void Suspend()
		{
			_executionEngine.Suspend();
		}

		public void Update(TimeSpan delta)
		{
			if (IsActive())
			{
				_executionEngine.Update(delta);
			}
		}

		public void Dispose()
		{
			_executionEngine.Dispose();
		}

		public IExecutionToken Execute(ITask task)
		{
			return _executionEngine.Execute(task);
		}
	}
}
