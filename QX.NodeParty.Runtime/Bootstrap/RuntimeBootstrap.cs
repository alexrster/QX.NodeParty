using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QX.NodeParty.Composition;
using QX.NodeParty.Composition.Bootstrap;
using QX.NodeParty.Config;
using QX.NodeParty.Runtime.CommandLine;
using QX.NodeParty.Runtime.Registry;

namespace QX.NodeParty.Runtime.Bootstrap
{
  public static class RuntimeBootstrap
  {
    public static Task<INode> Start(string[] arguments, NodeUri nodeUri = null)
    {
      Debug.Print("Parse command line arguments");
      var settings = CommandLineParser.Parse<NodeHostSettings>(arguments);

      Debug.Print("Create Hosting Runtime instance");
      var runtime = FullFrameworkNodeRuntime.CreateFromCurrentAppDomain();

      Debug.Print("Load configuration data");
      var config = LoadConfigurationData(runtime, settings);
      
      Debug.Print("Load root Node");
      return LoadRootNode(new NodeUri(nodeUri ?? NodeUri.LocalHost, settings.InstanceId ?? "/"), runtime, config);
    }

    private static NodeConfigurationData LoadConfigurationData(INodeRuntime runtime, NodeHostSettings settings)
    {
      Debug.Indent();

      Debug.Print("Create Node Config Loader of type '{0}'", settings.ConfigurationLoader);
      var configurationLoader = runtime.CreateObject<IConfigurationLoader>(settings.ConfigurationLoader);

      Debug.Print("Load Node Configuration from '{0}'", settings.ConfigLocations);
      var result = configurationLoader.Load(settings.ConfigLocations.First());

      Debug.Unindent();
      return result;
    }

    private static async Task<INode> LoadRootNode(NodeUri rootUri, INodeRuntime runtime, NodeConfigurationData configuration)
    {
      Debug.Indent();
      Debug.Print("Load Node Descriptors Loaders");

      //var nodeFactoryLoaders = configuration["nodeFactoryLoaders"].GetValue<IEnumerable<string>>()
      //  .Select(x => runtime.TypeLoader.LoadType(x))
      //  .Select(x => runtime.ObjectLoader.CreateInstance<INodeFactoryDescriptorLoader>(x))
      //  .ToArray();

      var nodeFactoryLoaders = new[] {new NodeFactoryDescriptorByTypeAttributeLoader(runtime)};

      Debug.Print("Create Node loader");
      var nodeLoader = new NodeFactoryLoader(nodeFactoryLoaders);

      Debug.Print("Create root Node locator builder");
      var nodeLocatorBuilder = new NodeLocatorBuilder();

      Debug.Print("Load root Node factory");
      var nodeFactory = nodeLoader.LoadNodeFactory(nodeLocatorBuilder, configuration);
      Debug.Assert(nodeFactory != null, "Unable to load root Node factory");

      Debug.Print($"Build root Node locator @'{rootUri}'");
      var locator = await nodeLocatorBuilder.BuildNodeLocator(rootUri);
      Debug.Assert(locator != null, "Unable to build root Node locator");

      Debug.Print($"Create root Node instance @'{rootUri}'");
      var node = await nodeFactory(rootUri, locator);
      Debug.Assert(node != null, "Unable to create root Node instance");

      Debug.Unindent();
      return node;
    }
  }
}
