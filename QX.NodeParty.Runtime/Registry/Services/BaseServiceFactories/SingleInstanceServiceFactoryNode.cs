using System;
using System.Threading.Tasks;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services.BaseServiceFactories
{
  public class SingleInstanceServiceContainerNode<TService> : ServiceContainerNode<TService>
    where TService : class
  {
    private readonly Lazy<Task<TService>> _lazyInstance;

    public SingleInstanceServiceContainerNode(NodeUri nodeUri, IServiceLocator serviceLocator, Func<IServiceLocator, Task<TService>> factory)
      : base(nodeUri, serviceLocator, factory)
    {
      _lazyInstance = new Lazy<Task<TService>>(() => base.GetInstance());
    }

    public override Task<TService> GetInstance(string data = null)
    {
      return _lazyInstance.Value;
    }
  }
}
