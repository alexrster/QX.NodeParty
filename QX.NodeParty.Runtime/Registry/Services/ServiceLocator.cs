using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services
{
  sealed class ServiceLocator : IServiceLocator
  {
    private readonly INodeLocator _nodeLocator;

    public ServiceLocator(INodeLocator nodeLocator)
    {
      _nodeLocator = nodeLocator;
    }

    public static string GetServiceId<TService>() where TService : class
    {
      return typeof(TService).Name;
    }

    public async Task<TService> GetService<TService>(string data) where TService : class
    {
      Debug.Indent();
      Debug.Print("Get service instance of type '{0}' with data: '{1}'", typeof(TService), data);

      var serviceId = GetServiceId<TService>();
      Debug.Print($"Using '{serviceId}' as serviceId for service of type '{typeof(TService)}'");

      var node = await _nodeLocator.GetNode(new NodeUri($"./{serviceId}", UriKind.Relative));
      Debug.Print($"Node for service '{serviceId}' of type '{typeof(TService)}' {(node != null?$"found, Node @'{node.NodeUri}' of type '{node.GetType()}'":"NOT found")}");

      if (node == null)
      {
        Debug.Unindent();
        return null;
      }

      TService instance;
      var serviceContainerNode = node as IServiceContainerNode<TService>;
      if (serviceContainerNode != null)
      {
        Debug.Print($"Get instance of service from ServiceContainer Node @'{serviceContainerNode.NodeUri}'");
        instance = await serviceContainerNode.GetInstance(data);

        Debug.Unindent();
        return instance;
      }
      
      instance = node as TService;
      Debug.Print($"Node @'{node.NodeUri}' is not ServiceContainer trying to cast it to '{typeof(TService)}' - '{instance}'");

      Debug.Unindent();
      return instance;
    }
  }
}
