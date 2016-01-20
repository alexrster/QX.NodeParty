using System;
using System.Threading.Tasks;
using QX.NodeParty.Config;

namespace QX.NodeParty.Composition
{
  public interface INodeFactoryDescriptor
  {
    Func<NodeUri, INodeLocator, Task<INode>> CreateNodeFactory(INodeLocatorRegistry registry, ConfigurationData configuration);
  }
}
