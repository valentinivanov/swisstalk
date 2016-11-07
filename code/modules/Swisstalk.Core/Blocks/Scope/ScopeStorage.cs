using System;
using System.Collections.Generic;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Pooling.Scope;
using Swisstalk.Foundation.Pooling.Scope.Disposable;

namespace Swisstalk.Core.Blocks.Scope
{
	public class ScopeStorage : IBlock, IDisposable, IScopeLocator, IScopeRegistrar
	{
		private readonly BlockId _instanceId;

		private readonly Dictionary<ScopeId, Scope<DisposableScopeFrame>> _scopes;

		public ScopeStorage(BlockId instanceId)
		{
			_instanceId = instanceId;
            _scopes = new Dictionary<ScopeId, Scope<DisposableScopeFrame>>();
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
			foreach (Scope<DisposableScopeFrame> scope in _scopes.Values)
			{
				while (!scope.IsEmpty())
				{
					scope.PopFrame();
				}
			}

			_scopes.Clear();
		}

		public Scope<DisposableScopeFrame> NamedScope(ScopeId scopeId)
		{
			return _scopes[scopeId];
		}

		public void Pop(ScopeId scopeId)
		{
			Scope<DisposableScopeFrame> scope = NamedScope(scopeId);
			scope.PopFrame();

			if (scope.IsEmpty())
			{
				_scopes.Remove(scopeId);
			}
		}

		public Scope<DisposableScopeFrame> Push(ScopeId scopeId)
		{
			if (!_scopes.ContainsKey(scopeId))
			{
				_scopes.Add(scopeId, new Scope<DisposableScopeFrame>());	
			}

			return NamedScope(scopeId).PushFrame();
		}
	}
}
