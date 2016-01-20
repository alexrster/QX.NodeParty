using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Runtime.Registry
{
  class StaticNodeLocator : Composition.Base.Node, INodeLocator
  {
    private readonly IDictionary<NodeUri, NodeInstanceContainer> _nodes;

    public StaticNodeLocator(NodeUri nodeUri, IEnumerable<NodeInstanceContainer> nodes) : base(nodeUri)
    {
      _nodes = nodes.ToDictionary(x => x.NodeUri, x => x);
    }

    public Task<INode> GetNode(NodeUri nodeUri)
    {
      NodeInstanceContainer nodeInstanceContainer;
      if (_nodes.TryGetValue(nodeUri, out nodeInstanceContainer))
      {
        return nodeInstanceContainer.GetNode();
      }

      return Task.FromResult<INode>(null);
    }
  }
}
