using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;
using QX.NodeParty.Runtime.Registry.Services.BaseServiceFactories;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services
{
  public class ServiceLocatorRegistry
  {
    private readonly INodeLocatorRegistry _nodeLocatorRegistry;

    public ServiceLocatorRegistry(INodeLocatorRegistry nodeLocatorRegistry)
    {
      _nodeLocatorRegistry = nodeLocatorRegistry;
    }

    public void RegisterService<TService>(string serviceId, Func<IServiceLocator, Task<TService>> serviceFactory) where TService : class
    {
      Debug.Print($"Register service of type '{typeof(TService)}' as service factory node with id '{serviceId}'");
      _nodeLocatorRegistry.RegisterNodeFactory(serviceId, (nodeUri, nodeLocator) => CreateServiceFactoryNode(nodeUri, nodeLocator, serviceFactory));
    }

    private Task<INode> CreateServiceFactoryNode<TService>(NodeUri nodeUri, INodeLocator nodeLocator, Func<IServiceLocator, Task<TService>> serviceFactory) where TService : class
    {
      Debug.Print($"Create Service Factory Node for service of type '{typeof(TService)}' at '{nodeUri}'");
      return Task.FromResult<INode>(new SingleInstanceServiceContainerNode<TService>(nodeUri, new ServiceLocator(nodeLocator), serviceFactory));
    }
  }
}
