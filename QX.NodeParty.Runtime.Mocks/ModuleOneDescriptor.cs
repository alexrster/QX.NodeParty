using System.Threading.Tasks;
using NLog;
using QX.NodeParty.Config;
using QX.NodeParty.Runtime.Registry.Services;
using QX.NodeParty.Services;

namespace QX.NodeParty.Runtime.Mocks
{
  public class ModuleOneDescriptor : ServiceFactoryNodeDescriptor
  {
    protected override void Configure(ServiceLocatorRegistry registry, ConfigurationData configuration)
    {
    }

    protected override async Task Configure(IServiceLocator locator, ConfigurationData configuration)
    {
      var logger = await locator.GetService<ILogger>();
      logger.Trace("Configured test 'module one'");
    }
  }

  public class ModuleOneService
  { }
}
