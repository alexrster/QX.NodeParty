using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QX.NodeParty.Runtime.Bootstrap;

namespace QX.NodeParty.Runtime.Tests
{
  [TestClass]
  public class JsonLoaderTests
  {
    [TestMethod]
    public async Task TestJsonConfigurationParsed_Ok()
    {
      var environment = new []
      {
        "/cl:QX.NodeParty.Runtime.JsonConfig.JsonFileConfigurationLoader, QX.NodeParty.Runtime.dll",
        "/c:node.json",
        "/i:mstest"
      };

      var runtime = await RuntimeBootstrap.Start(environment);

      //await runtime.StartAsync(environment);

      //Assert.IsNotNull(runtime.NodeConfigData.Type);
      //Assert.AreNotEqual(string.Empty, runtime.NodeConfigData.Type);

      //Assert.IsNotNull(runtime.NodeConfigData.Nodes);
      //Assert.IsTrue(runtime.NodeConfigData.Nodes.Any());

      //Assert.IsNotNull(runtime.Node);

      var disposableRuntime = runtime as IDisposable;
      if (disposableRuntime != null)
        disposableRuntime.Dispose();
    }
  }
}
