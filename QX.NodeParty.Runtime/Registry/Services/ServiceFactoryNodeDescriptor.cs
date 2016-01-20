using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;
using QX.NodeParty.Config;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services
{
  public abstract class ServiceFactoryNodeDescriptor : INodeFactoryDescriptor
  {
    public Func<NodeUri, INodeLocator, Task<INode>> CreateNodeFactory(INodeLocatorRegistry registry, ConfigurationData configuration)
    {
      Debug.Print($"Create ServiceLocatorBuilder for service factory descriptor '{GetType().FullName}'");
      var serviceLocatorRegistry = new ServiceLocatorRegistry(registry);

      Debug.Print($"Configure NodeFactory Services of ServiceFactory Descriptor '{GetType().FullName}'");
      Configure(serviceLocatorRegistry, configuration);

      return (nodeUri, nodeLocator) => Task.Run(async () =>
      {
        Debug.Print($"Configure service dependencies for Node at '{nodeUri}'");
        await Configure(new ServiceLocator(nodeLocator), configuration);

        Debug.Print($"All done for Node at '{nodeUri}' - create an instance");
        return (INode)nodeLocator;
      });
    }

    protected virtual void Configure(ServiceLocatorRegistry registry, ConfigurationData configuration)
    { }

    protected virtual Task Configure(IServiceLocator locator, ConfigurationData configuration)
    {
      return Task.FromResult(0);
    }
  }
}
