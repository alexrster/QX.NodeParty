using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services.BaseServiceFactories
{
  public class InstancePerRequestServiceContainerNode<TService> : ServiceContainerNode<TService>
    where TService : class
  {
    private readonly ConcurrentDictionary<string, Task<TService>> _instances = new ConcurrentDictionary<string, Task<TService>>();

    public InstancePerRequestServiceContainerNode(NodeUri nodeUri, IServiceLocator serviceLocator, Func<IServiceLocator, Task<TService>> factory)
      : base(nodeUri, serviceLocator, factory)
    { }

    public override async Task<TService> GetInstance(string data = null)
    {
      if (string.IsNullOrWhiteSpace(data))
      {
        data = Guid.NewGuid().ToString("D");
      }

      return await _instances.GetOrAdd(data, async x => await base.GetInstance(data));
    }
  }
}
