using QX.NodeParty.Config;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node factory services descriptor loader
  /// </summary>
  public interface INodeFactoryDescriptorLoader
  {
    /// <summary>
    /// Load Node factory services descriptor from configuration node
    /// </summary>
    /// <param name="configuration">Node factory services configuration node</param>
    /// <returns>Node factory services descriptor</returns>
    INodeFactoryDescriptor LoadNodeFactoryDescriptor(NodeConfigurationData configuration);
  }
}
