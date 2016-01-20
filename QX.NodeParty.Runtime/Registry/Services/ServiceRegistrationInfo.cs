using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Registry.Services
{
  public abstract class ServiceFactoryRegistrationInfo
  {
    public abstract Task<object> CreateService(IServiceLocator serviceLocator);
  }

  public class ServiceFactoryRegistrationInfo<TService> : ServiceFactoryRegistrationInfo where TService : class 
  {
    private readonly string _serviceFactoryId;
    private readonly Func<IServiceLocator, Task<TService>> _serviceFactory;

    public ServiceFactoryRegistrationInfo(string serviceFactoryId, Func<IServiceLocator, Task<TService>> serviceFactory)
    {
      _serviceFactoryId = serviceFactoryId;
      _serviceFactory = serviceFactory;
    }

    public override async Task<object> CreateService(IServiceLocator serviceLocator)
    {
      Debug.Print("Create instance of '{0}'", typeof(TService).Name);
      return await _serviceFactory(serviceLocator);
    }
  }
}
