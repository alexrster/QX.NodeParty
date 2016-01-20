using System.Threading.Tasks;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node locator builder
  /// </summary>
  public interface INodeLocatorBuilder : INodeLocatorRegistry
  {
    /// <summary>
    /// Compose Node locator
    /// </summary>
    /// <param name="nodeUri">Node locator URI</param>
    /// <returns>Node Locator instance</returns>
    Task<INodeLocator> BuildNodeLocator(NodeUri nodeUri);
  }
}
