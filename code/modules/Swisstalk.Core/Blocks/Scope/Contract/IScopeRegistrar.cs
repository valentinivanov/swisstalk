using Swisstalk.Foundation.Pooling.Scope;
using Swisstalk.Foundation.Pooling.Scope.Disposable;

namespace Swisstalk.Core.Blocks.Scope
{
	public interface IScopeRegistrar
	{
		Scope<DisposableScopeFrame> Push(ScopeId scopeId);
		void Pop(ScopeId scopeId);
	}
}
