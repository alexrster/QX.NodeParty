using QX.NodeParty.Config;

namespace QX.NodeParty.Composition
{
  public interface INodeFactoryDescriptorLoader
  {
    INodeFactoryDescriptor LoadNodeFactoryDescriptor(NodeConfigurationData configuration);
  }
}
