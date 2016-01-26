using System;
using System.Threading.Tasks;
using QX.NodeParty.Config;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node factory services descriptor
  /// </summary>
  public interface INodeFactoryDescriptor
  {
    /// <summary>
    /// Create Node factory services for specific configuration
    /// </summary>
    /// <param name="registry">Registry where Node factory services will be registered</param>
    /// <param name="configuration">Node factory services configuration</param>
    /// <returns>Node factory</returns>
    Func<NodeUri, INodeLocator, Task<INode>> CreateNodeFactory(INodeLocatorRegistry registry, ConfigurationData configuration);
  }
}
