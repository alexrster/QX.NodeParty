using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services
{
  public class ServiceContainerNode<TService> : Composition.Base.Node, IServiceContainerNode<TService>
    where TService : class
  {
    private readonly IServiceLocator _serviceLocator;
    protected Func<IServiceLocator, Task<TService>> Factory { get; }

    protected ServiceContainerNode(NodeUri nodeUri, IServiceLocator serviceLocator, Func<IServiceLocator, Task<TService>> factory) : base(nodeUri)
    {
      _serviceLocator = serviceLocator;
      Factory = factory;
    }

    public virtual async Task<TService> GetInstance(string data = null)
    {
      Debug.Print($"Service Locator OK - invoke Service Factory of '{typeof(TService)}'");
      return await Factory(_serviceLocator);
    }
  }
}
