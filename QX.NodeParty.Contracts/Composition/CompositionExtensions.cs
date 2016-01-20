using System;
using System.Threading;

namespace QX.NodeParty.Composition
{
  public static class CompositionExtensions
  {
    public static Mutex AcquireMutex(this Base.Node node, TimeSpan timeout)
    {
      return Base.Node.AcquireMutex(node.NodeUri, timeout);
    }

    //public static INodeLocatorRegistry RegisterServiceFactory<TService>(this INodeLocatorRegistry locatorRegistry, string serviceId, Func<INodeLocator, Task<TService>> serviceFactory) where TService : class
    //{
    //  locatorRegistry.RegisterNodeFactory(serviceId, locator => Task.FromResult<INode>(
    //        new InstancePerRequestServiceFactory<TService>(
    //          new NodeUri(locator.NodeUri, serviceId), 
    //          serviceFactory)));

    //  return locatorRegistry;
    //}

    //public static async Task<TService> GetServiceAsync<TService>(this INodeLocator nodeLocator)
    //    where TService : class
    //{
    //  return (TService)await nodeLocator.GetNodeAsync(new NodeUri(nodeLocator.NodeUri, typeof(TService).FullName));
    //}

    //public static void RegisterService<TService>(this ContainerNodeDescriptor containerNodeDescriptor, Func<INodeLocator, TService> serviceFactory)
    //    where TService : class
    //{
    //  var descriptor = new DefaultNodeFactoryDesscriptor<TService>();
    //  var serviceNodeFactory = serviceFactory as Func<INodeLocator, INode> ?? new NodeFactory<TService>(containerNodeDescriptor, containerNode => serviceFactory(containerNode));

    //  containerNodeDescriptor.AddNodeFactory(new NodeUri(containerNodeDescriptor.NodeUri, Base.Node.CreateNodeId(typeof(TService))), serviceFactory);
    //}

    //public static void RegisterService<TService>(this NodeModuleBuilder builder, INode node, Func<INode, TService> serviceFactoryFunc)
    //    where TService : class
    //{
    //  builder.AddServiceRegistration(new ServiceAsTypeRegistration<TService>(new NodeServiceFactoryFuncActivator(node, serviceFactoryFunc)));
    //}

    //public static void RegisterService<TService>(this ContainerNodeDescriptor builder, Func<INode, TService> serviceFactory)
    //{
    //  builder.AddNodeFactory(typeof(TService), node => () => serviceFactory(node));
    //}

    //public static void RegisterService<TService>(this ServiceContainerBuilder builder, Func<TService> serviceFactory)
    //{
    //  builder.AddServiceFactoryDescriptor(typeof(TService), node => () => serviceFactory());
    //}

    //public static void RegisterService<TService>(this ServiceContainerBuilder builder, TService serviceInstance)
    //{
    //  builder.AddServiceFactoryDescriptor(typeof(TService), node => () => serviceInstance);
    //}
  }
}
