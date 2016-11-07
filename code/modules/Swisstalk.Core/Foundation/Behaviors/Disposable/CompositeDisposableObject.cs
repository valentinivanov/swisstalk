using System;
using Swisstalk.Foundation.Composition;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Behaviors.ActiveObject
{
	public class CompositeDisposableObject : UniqueReversibleComposition<IDisposable>, IDisposable
	{
		private bool _disposed;

		public CompositeDisposableObject()
		{
			_disposed = false;
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				foreach (IDisposable disposable in Composite)
				{
					disposable.Dispose();
				}

				_disposed = true;
			}
		}
	}
}
