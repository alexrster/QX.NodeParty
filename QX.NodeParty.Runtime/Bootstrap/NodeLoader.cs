using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using QX.NodeParty.Composition;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.Bootstrap
{
  class NodeFactoryLoader
  {
    private readonly IEnumerable<INodeFactoryDescriptorLoader> _descriptorLoaders;

    public NodeFactoryLoader(IEnumerable<INodeFactoryDescriptorLoader> descriptorLoaders)
    {
      _descriptorLoaders = descriptorLoaders;
    }

    private INodeFactoryDescriptor LoadDescriptor(NodeConfigurationData configuration)
    {
      foreach (var loader in _descriptorLoaders)
      {
        try
        {
          Debug.Print("Try load Node factory descriptor using loader of type '{0}'", loader.GetType());
          var descriptor = loader.LoadNodeFactoryDescriptor(configuration);
          if (descriptor != null)
          {
            Debug.Print("Successfully loaded Node factory descriptor of type '{0}'", descriptor.GetType());
            return descriptor;
          }

        }
        catch (Exception e)
        {
          Debug.WriteLine(e.Message);
          return null;
        }
      }

      Debug.Fail("There are no loaders configured that able to load Node factory descriptor using configuration data", configuration.GetValue<string>());
      return null;
    }

    public Func<NodeUri, INodeLocator, Task<INode>> LoadNodeFactory(INodeLocatorRegistry registry, NodeConfigurationData configuration)
    {
      Debug.Indent();
      Debug.Assert(configuration != null, "Node configuration is NULL");

      Debug.Print("Load Node Factory Descriptor");
      var descriptor = LoadDescriptor(configuration);
      Debug.Assert(descriptor != null, "Unknown or invalid Node descriptor");

      Debug.Print("Load child Node Factories");
      var childTasks = new Dictionary<string, Task<INodeLocatorRegistry>>();
      int childNodeAutoId = 0;
      foreach (var nodeConfiguration in configuration.Nodes)
      {
        childNodeAutoId++;

        var config = nodeConfiguration;
        var id = config.Id ?? childNodeAutoId.ToString("D2");

        if (childTasks.ContainsKey(id))
        {
          id = $"{id}-{childNodeAutoId}";
        }

        Debug.Print("Load Node at '{0}'", id);
        childTasks[id] = Task.Run(() =>
        {
          Debug.Print("Create Node Locator Builder for child node '{0}'", id);
          var childNodeLocatorRegistry = registry.CreateChildRegistry(id);

          Debug.Print("Load Node Factory for child node '{0}'", id);
          var childNodeFactory = LoadNodeFactory(childNodeLocatorRegistry, config);

          Debug.Print("Rregister child Node Factory '{0}'", id);
          registry.RegisterNodeFactory(id, async (uri, locator) => await childNodeFactory(uri, locator));
          
          return childNodeLocatorRegistry;
        });
      }

      var nodeFactory = descriptor.CreateNodeFactory(registry, configuration);

      var task = new Func<NodeUri, INodeLocator, Task<INode>>(async (uri, locator) =>
      {
        Debug.Indent();
        Debug.Print("Wait for all child Node Factories of '{0}' to finish initialization", uri);
        await Task.WhenAll(childTasks.Values);

        Debug.Print("Create Node instance at '{0}'", uri);
        var node = await nodeFactory(uri, locator);

        Debug.Unindent();
        return node;
      });

      Debug.Unindent();
      return task;
    }
  }
}
