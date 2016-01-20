using System.Collections.Generic;

namespace QX.NodeParty.Config
{
  public abstract class NodeConfigurationData : ConfigurationData
  {
    public abstract string Id { get; }
    public abstract IEnumerable<NodeConfigurationData> Nodes { get; }

    public abstract T GetValue<T>(string key);
  }
}
