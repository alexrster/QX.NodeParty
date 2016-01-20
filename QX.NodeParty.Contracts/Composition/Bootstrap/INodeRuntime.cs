using System;

namespace QX.NodeParty.Composition.Bootstrap
{
  /// <summary>
  /// Node hosting environment
  /// </summary>
  public interface INodeRuntime : IDisposable
  {
    /// <summary>
    /// Create new instance of object using factory identified by <paramref name="objectFactoryId"/>
    /// </summary>
    /// <param name="objectFactoryId">Object factory ID</param>
    /// <returns>New object instance</returns>
    object CreateObject(string objectFactoryId);
  }
}
