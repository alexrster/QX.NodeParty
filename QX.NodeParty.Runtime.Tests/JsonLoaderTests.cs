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
        "/c:node.json"
      };

      var runtime = await RuntimeBootstrap.Start(environment);

      var disposableRuntime = runtime as IDisposable;
      if (disposableRuntime != null)
        disposableRuntime.Dispose();
    }
  }
}
