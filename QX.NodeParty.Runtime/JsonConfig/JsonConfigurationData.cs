using Newtonsoft.Json.Linq;
using QX.NodeParty.Config;

namespace QX.NodeParty.Runtime.JsonConfig
{
  class JsonConfigurationData : ConfigurationData
  {
    private readonly JToken _jToken;
    private readonly ConfigurationData _parentConfigurationData;

    public override ConfigurationData this[string key]
    {
      get { return new JsonConfigurationData(_jToken[key], this); }
    }

    public JsonConfigurationData(JToken jToken, ConfigurationData parentConfigurationData)
    {
      _jToken = jToken;
      _parentConfigurationData = parentConfigurationData;
    }

    public override T GetValue<T>()
    {
      var value = _jToken.Value<T>();
      if (value.Equals(default(T)) && _parentConfigurationData != null)
      {
        return _parentConfigurationData.GetValue<T>();
      }

      return value;
    }
  }
}
