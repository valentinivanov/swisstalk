using System;
using System.Collections.Generic;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Core.Blocks.Support.Discovery
{
	public class BlockResolutionContext : IBlockResolutionContext
    {
        private readonly Dictionary<BlockId, IBlock> _blockMap;
		private ResolutionContextState _state;

		public BlockResolutionContext()
        {
            _blockMap = new Dictionary<BlockId, IBlock>();
			_state = ResolutionContextState.Inactive;
        }

		public ResolutionContextState ContextState
		{
			get
			{
				return _state;
			}
		}

		public void Activate()
		{
            EnsureState(ResolutionContextState.Inactive);

            _state = ResolutionContextState.Active;
		}

		public void Append(IBlock item)
        {
			EnsureState(ResolutionContextState.Inactive);

            _blockMap[item.InstanceId] = item;
        }

		public void Deactivate()
		{
            EnsureState(ResolutionContextState.Active);

            _state = ResolutionContextState.Inactive;
		}

		public void Remove(IBlock item)
        {
			EnsureState(ResolutionContextState.Inactive);

			_blockMap.Remove(item.InstanceId);
		}

        public IBlock Resolve(BlockId serviceId)
        {
            EnsureState(ResolutionContextState.Active);

            IBlock service = null;
            if (_blockMap.TryGetValue(serviceId, out service))
            {
                return service;
            }
            else
            {
                return null;
            }
        }

		private void EnsureState(ResolutionContextState state)
		{
            if (_state != state)
			{
				throw new InvalidOperationException();
			}
		}
    }
}
