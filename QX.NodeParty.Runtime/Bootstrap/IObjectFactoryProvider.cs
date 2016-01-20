using System;

namespace QX.NodeParty.Runtime.Bootstrap
{
  /// <summary>
  /// Provide a factory of objects by ID
  /// </summary>
  public interface IObjectFactoryProvider
  {
    /// <summary>
    /// Get an objects factory by <paramref name="id"/>
    /// </summary>
    /// <param name="id">Object factory ID</param>
    /// <returns>Object factory instance</returns>
    Func<object> GetObjectFactory(string id);
  }
}
