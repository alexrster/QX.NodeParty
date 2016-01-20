using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Runtime.Registry
{
  public class LinkedAsyncNodeLocator : INodeLocator
  {
    private readonly INodeLocator _nodeLocator;
    private readonly Task<INodeLocator> _linkedNodeLocatorTask;

    public NodeUri NodeUri => _nodeLocator.NodeUri;

    public LinkedAsyncNodeLocator(INodeLocator nodeLocator, Task<INodeLocator> linkedNodeLocatorTask)
    {
      _nodeLocator = nodeLocator;
      _linkedNodeLocatorTask = linkedNodeLocatorTask;
    }

    public async Task<INode> GetNode(NodeUri nodeUri)
    {
      Debug.Indent();
      Debug.Print($"Node locator @'{NodeUri}' - get Node instance @'{nodeUri}'");
      var node = await _nodeLocator.GetNode(nodeUri);
      if (node != null)
      {
        Debug.Print($"Node locator @'{NodeUri}' - Node instance @'{nodeUri}' found");
        return node;
      }

      Debug.Print($"Node locator @'{NodeUri}' - Node instance @'{nodeUri}' NOT FOUND");
      Debug.Print($"Node locator @'{NodeUri}' - Get or wait for linked Node locator");
      var linkedLocator = await _linkedNodeLocatorTask;

      Debug.Print($"Acquired linked Node locator @'{linkedLocator.NodeUri}'");
      node = await linkedLocator.GetNode(nodeUri);

      Debug.Unindent();
      return node;
    }
  }
}
