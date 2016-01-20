using System;
using System.Threading.Tasks;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node locator registry
  /// </summary>
  public interface INodeLocatorRegistry
  {
    /// <summary>
    /// Register Node Factory configuration in the Node locator
    /// </summary>
    /// <param name="nodeId">Node ID</param>
    /// <param name="nodeFactory">Node factory</param>
    void RegisterNodeFactory(string nodeId, Func<NodeUri, INodeLocator, Task<INode>> nodeFactory);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    INodeLocatorRegistry CreateChildRegistry(string nodeId);
  }
}
