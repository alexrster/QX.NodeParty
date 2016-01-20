using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace QX.NodeParty.Runtime.Bootstrap
{
  class CachedObjectFactoryProvider : IObjectFactoryProvider
  {
    private readonly IObjectFactoryProvider[] _providers;
    private readonly ConcurrentDictionary<string, Func<object>> _objectFactoriesCache = new ConcurrentDictionary<string, Func<object>>();

    public CachedObjectFactoryProvider(params IObjectFactoryProvider[] providers)
    {
      _providers = providers;
    }

    public Func<object> GetObjectFactory(string id)
    {
      return _objectFactoriesCache.GetOrAdd(id, CreateObjectFactory);
    }

    private Func<object> CreateObjectFactory(string id)
    {
      var factory = _providers.Select(x => x.GetObjectFactory(id)).FirstOrDefault(x => x != null);
      Debug.Assert(factory != null, "Object factory provider not found", "No object factory '{0}' provider found", id);

      return factory;
    }
  }
}