using QX.NodeParty.Composition;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.Bootstrap
{
  public abstract class NodeFactoryDescriptorByConfigurationAttributeLoader : INodeFactoryDescriptorLoader
  {
    public string AttributeName { get; private set; }

    public NodeFactoryDescriptorByConfigurationAttributeLoader(string attributeName)
    {
      AttributeName = attributeName;
    }

    public abstract INodeFactoryDescriptor LoadNodeFactoryDescriptor(NodeConfigurationData configuration);
  }
}
