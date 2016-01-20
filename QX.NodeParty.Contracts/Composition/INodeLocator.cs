using System.Threading.Tasks;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node provider
  /// </summary>
  public interface INodeLocator : INode
  {
    /// <summary>Get Node instance or proxy by URI</summary>
    /// <param name="nodeUri">Target Node URI</param>
    /// <returns>Asynchronously return Node instance or proxy if found, otherwise NULL</returns>
    Task<INode> GetNode(NodeUri nodeUri);
  }
}
