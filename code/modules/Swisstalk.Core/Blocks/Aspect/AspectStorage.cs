using System;
using System.Collections.Generic;
using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Foundation.Algorithms;
using Swisstalk.Foundation.Factory;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Core.Blocks.Aspect
{
	public class AspectStorage : IBlock, IAspectLocator, IAspectRegistrar
	{
		private class PerTypeStorageFactory : IFactory<Dictionary<Type, IAspect>>
		{
			public Dictionary<Type, IAspect> Execute()
			{
				return new Dictionary<Type, IAspect>();
			}
		}

		private readonly BlockId _instanceId;

		private readonly Dictionary<string, Dictionary<Type, IAspect>> _aspects;
		private readonly KeySearchMethod<string, Dictionary<Type, IAspect>> _aspectSearch;
		private readonly PerTypeStorageFactory _perTypeStorageFactory;

		public AspectStorage(BlockId instanceId)
		{
			_instanceId = instanceId;
			
			_aspects = new Dictionary<string, Dictionary<Type, IAspect>>();
			_aspectSearch = new KeySearchMethod<string, Dictionary<Type, IAspect>>();
			_perTypeStorageFactory = new PerTypeStorageFactory();
		}

		public BlockId InstanceId
		{
			get
			{
				return _instanceId;
			}
		}

		public T Get<T>(string aspectId) where T : IAspect
		{
			Dictionary<Type, IAspect> perTypeStorage;
			if (_aspects.TryGetValue(aspectId, out perTypeStorage))
			{
				IAspect aspect;
				if (perTypeStorage.TryGetValue(typeof(T), out aspect) && 
				    aspect is T)
				{
					return (T)aspect;
				}
			}

			throw new AspectNotRegisteredException();
		}

		public void Register<T>(T aspect) where T : IAspect
		{
			Dictionary<Type, IAspect> perTypeStorage = _aspectSearch.GetOrCreate(aspect.Id, 
			                                                                     _aspects, 
			                                                                     _perTypeStorageFactory);
			
			perTypeStorage[typeof(T)] = aspect;
		}

		public void Unregister<T>(string aspectId) where T : IAspect
		{
			_aspects.Remove(aspectId);
		}
	}
}
