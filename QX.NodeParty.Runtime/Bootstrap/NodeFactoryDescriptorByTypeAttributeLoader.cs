using QX.NodeParty.Composition;
using QX.NodeParty.Composition.Bootstrap;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.Bootstrap
{
  public class NodeFactoryDescriptorByTypeAttributeLoader : NodeFactoryDescriptorByConfigurationAttributeLoader
  {
    private readonly INodeRuntime _runtime;

    public NodeFactoryDescriptorByTypeAttributeLoader(INodeRuntime runtime) : base("descriptor")
    {
      _runtime = runtime;
    }

    public override INodeFactoryDescriptor LoadNodeFactoryDescriptor(NodeConfigurationData configuration)
    {
      return (INodeFactoryDescriptor)_runtime.CreateObject(configuration.GetValue<string>(AttributeName));
    }
  }
}
