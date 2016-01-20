using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Runtime.Registry
{
  class NodeInstanceContainer
  {
    private readonly Task<INode> _nodeInstanceTask;

    public NodeUri NodeUri { get; }

    public NodeInstanceContainer(NodeUri nodeUri, Task<INode> nodeInstanceTask)
    {
      NodeUri = nodeUri;
      _nodeInstanceTask = nodeInstanceTask;
    }

    public Task<INode> GetNode()
    {
      return _nodeInstanceTask;
    }
  }
}
