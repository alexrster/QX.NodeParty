using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QX.NodeParty.Composition.Bootstrap;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.JsonConfig
{
  public class JsonFileConfigurationLoader : IConfigurationLoader
  {
    public NodeConfigurationData Load(string param = null)
    {
      Debug.Assert(!string.IsNullOrEmpty(param), "Configuration URI parameter is null or empty");

      var configurationLocationUri = new Uri(param, UriKind.RelativeOrAbsolute);
      if (!configurationLocationUri.IsAbsoluteUri)
      {
        configurationLocationUri = new Uri(Path.Combine(Environment.CurrentDirectory, configurationLocationUri.ToString()), UriKind.Absolute);
      }

      if (!configurationLocationUri.IsFile)
      {
        throw new UriFormatException("File URI expected");
      }

      return new JsonNodeConfigurationData(JsonConvert.DeserializeObject<JToken>(File.ReadAllText(configurationLocationUri.LocalPath)), null);
    }
  }
}
