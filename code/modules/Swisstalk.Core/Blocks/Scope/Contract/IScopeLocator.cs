using Swisstalk.Foundation.Pooling.Scope;
using Swisstalk.Foundation.Pooling.Scope.Disposable;

namespace Swisstalk.Core.Blocks.Scope
{
	public interface IScopeLocator
	{
		Scope<DisposableScopeFrame> NamedScope(ScopeId scopeId);
	}
}
