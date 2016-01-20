using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.JsonConfig
{
  class JsonNodeConfigurationData : NodeConfigurationData
  {
    private readonly JToken _jToken;
    private readonly ConfigurationData _parentConfig;
    private readonly Lazy<IDictionary<string, ConfigurationData>> _lazyConfig;
    private readonly Lazy<IEnumerable<NodeConfigurationData>> _lazyNodes;

    public override string Id
    {
      get { return _jToken.Value<string>("id"); }
    }
    
    public override IEnumerable<NodeConfigurationData> Nodes
    {
      get { return _lazyNodes.Value; }
    }

    public override ConfigurationData this[string key]
    {
      get
      {
        ConfigurationData value;
        return _lazyConfig.Value.TryGetValue(key, out value) ? 
          value : 
          _parentConfig != null ? 
          _parentConfig[key] : 
          Empty;
      }
    }

    public override T GetValue<T>()
    {
      return _jToken.Value<T>();
    }

    public override T GetValue<T>(string key)
    {
      return _jToken.Value<T>(key);
    }

    public JsonNodeConfigurationData(JToken jToken, ConfigurationData parentConfig)
    {
      _jToken = jToken;
      _parentConfig = parentConfig;

      _lazyConfig = new Lazy<IDictionary<string, ConfigurationData>>(
        () => 
          _jToken.Value<JObject>("config")
          ?.Properties().ToDictionary(x => x.Name, x => (ConfigurationData) new JsonConfigurationData(x.Value, this)) 
          ?? new Dictionary<string, ConfigurationData>());

      _lazyNodes = new Lazy<IEnumerable<NodeConfigurationData>>(
        () => 
          _jToken.Value<JArray>("nodes")
          ?.Select(x => new JsonNodeConfigurationData(x, this)) 
          ?? Enumerable.Empty<NodeConfigurationData>());
    }
  }
}
