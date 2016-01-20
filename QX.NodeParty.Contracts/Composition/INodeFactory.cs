using System.Threading.Tasks;

namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Node factory
  /// </summary>
  public interface INodeFactory : INode
  {
    /// <summary>
    /// Create new instance of the Node
    /// </summary>
    /// <returns></returns>
    Task<INode> CreateNode();
  }
}
