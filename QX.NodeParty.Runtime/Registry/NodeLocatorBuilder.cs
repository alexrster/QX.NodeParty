using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Runtime.Registry
{
  class NodeLocatorBuilder : INodeLocatorRegistry
  {
    private readonly LinkedList<KeyValuePair<string, NodeInstanceContainerBuilder>> _nodes = new LinkedList<KeyValuePair<string, NodeInstanceContainerBuilder>>();
    private readonly TaskCompletionSource<INodeLocator> _nodeLocatorCompletionSource = new TaskCompletionSource<INodeLocator>();

    public void RegisterNodeFactory(string nodeId, Func<NodeUri, INodeLocator, Task<INode>> nodeFactory)
    {
      Debug.Print("Create async wrapper for Node '{0}'", nodeId);
      var nodeContainer = new NodeInstanceContainerBuilder(nodeFactory);

      Debug.Print($"Add Node '{nodeId}' to internal collection");
      _nodes.AddLast(new KeyValuePair<string, NodeInstanceContainerBuilder>(nodeId, nodeContainer));
    }

    public Task<INodeLocator> BuildNodeLocator(NodeUri nodeUri)
    {
      Debug.Indent();
      Debug.Print($"Build NodeLocator @'{nodeUri}'");
      var defaultLocator = new DefaultNodeLocator(nodeUri);

      var definedNodes = new KeyValuePair<string, NodeInstanceContainerBuilder>[_nodes.Count];
      _nodes.CopyTo(definedNodes, 0);

      var nodeInstanceContainers = new LinkedList<NodeInstanceContainer>();
      foreach (var nodeBuilder in definedNodes)
      {
        var container = nodeBuilder.Value.BuildContainer(new NodeUri(nodeUri, nodeBuilder.Key), defaultLocator);
        nodeInstanceContainers.AddLast(container);
      }

      var locator = new StaticNodeLocator(nodeUri, nodeInstanceContainers);

      Debug.Print($"NodeLocator @'{nodeUri}' built successfully");
      _nodeLocatorCompletionSource.SetResult(locator);

      Debug.Unindent();
      return Task.FromResult<INodeLocator>(locator);
    }

    public INodeLocatorRegistry CreateChildRegistry(string nodeId)
    {
      var childRegistry = new NodeLocatorBuilder();
      RegisterNodeFactory(nodeId, async (nodeUri, locator) => new LinkedAsyncNodeLocator(await childRegistry.BuildNodeLocator(nodeUri), _nodeLocatorCompletionSource.Task));

      return childRegistry;
    }
  }

  class DefaultNodeLocator : Composition.Base.Node, INodeLocator
  {
    public DefaultNodeLocator(NodeUri nodeUri) : base(nodeUri)
    { }

    public Task<INode> GetNode(NodeUri nodeUri)
    {
      return Task.FromResult<INode>(null);
    }
  }
}
