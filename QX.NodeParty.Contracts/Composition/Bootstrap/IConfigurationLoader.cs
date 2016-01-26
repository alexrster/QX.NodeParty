using QX.NodeParty.Config;

namespace QX.NodeParty.Composition.Bootstrap
{
  /// <summary>
  /// Configuration data loader
  /// </summary>
  public interface IConfigurationLoader
  {
    /// <summary>
    /// Load configuration data
    /// </summary>
    /// <param name="param">Configuration data loader parameter</param>
    /// <returns>Configuration data</returns>
    NodeConfigurationData Load(string param = null);
  }
}
