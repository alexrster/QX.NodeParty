using System;
using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Runtime.Registry
{
  class NodeInstanceContainerBuilder
  {
    private readonly Func<NodeUri, INodeLocator, Task<INode>> _nodeFactory;

    public NodeInstanceContainerBuilder(Func<NodeUri, INodeLocator, Task<INode>> nodeFactory)
    {
      _nodeFactory = nodeFactory;
    }

    public NodeInstanceContainer BuildContainer(NodeUri nodeUri, INodeLocator nodeLocator)
    {
      return new NodeInstanceContainer(nodeUri, _nodeFactory(nodeUri, nodeLocator));
    }
  }
}
