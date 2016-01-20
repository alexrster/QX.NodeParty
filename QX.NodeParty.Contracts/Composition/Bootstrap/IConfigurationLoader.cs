using QX.NodeParty.Config;

namespace QX.NodeParty.Composition.Bootstrap
{
  public interface IConfigurationLoader
  {
    NodeConfigurationData Load(string param = null);
  }
}
