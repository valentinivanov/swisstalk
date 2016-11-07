using System;
using System.Collections.Generic;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Tasks;

namespace Swisstalk.Core.Blocks.MetaScript
{
	public class MetaScriptEngine : IBlock, IMetaScriptEngine, IMetaScriptEngineConfigurator, IActiveObject, ISuspendable, IDisposable 
	{
		private readonly BlockId _instanceId;
		private readonly Dictionary<ExecutionEngineId, IExecutionEngine> _channels;

		private bool _active;
		
		public MetaScriptEngine(BlockId instanceId)
		{
			_instanceId = instanceId;
			_channels = new Dictionary<ExecutionEngineId, IExecutionEngine>();

			_active = true;
		}

		public BlockId InstanceId
		{
			get
			{
				return _instanceId;
			}
		}

		public void Dispose()
		{
			foreach (IExecutionEngine channel in _channels.Values)
			{
				IDisposable disposable = channel as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public bool IsActive()
		{
			return _active;
		}

		public void Resume()
		{
			_active = true;
		}

		public void Suspend()
		{
			_active = false;
		}

		public void Update(TimeSpan delta)
		{
			if (IsActive())
			{
				foreach (IExecutionEngine channel in _channels.Values)
				{
					channel.Update(delta);
				}
			}
		}

		public IExecutionEngine GetChannel(ExecutionEngineId channelId)
		{
			return _channels[channelId];
		}

		public void RegisterChannel(IExecutionEngine channel)
		{
			_channels[channel.Id] = channel; 
		}

		public void UnregisterChannel(ExecutionEngineId channelId)
		{
			_channels.Remove(channelId);
		}

		public void UnregisterChannel(IExecutionEngine channel)
		{
			_channels.Remove(channel.Id);
		}
	}
}
