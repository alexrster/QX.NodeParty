using System.Threading.Tasks;
using NLog;
using QX.NodeParty.Config;
using QX.NodeParty.Runtime.Registry.Services;

namespace QX.NodeParty.Logging.NLog
{
  public class ServiceNodeDescriptor : ServiceFactoryNodeDescriptor
  {
    protected override void Configure(ServiceLocatorRegistry registry, ConfigurationData configuration)
    {
      registry.RegisterService(
        typeof(ILogger).Name,
        locator => Task.FromResult<ILogger>(LogManager.GetLogger("Default")));
    }
  }
}
