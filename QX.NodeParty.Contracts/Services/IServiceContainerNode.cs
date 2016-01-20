using System.Threading.Tasks;
using QX.NodeParty.Composition;

namespace QX.NodeParty.Services
{
  /// <summary>
  /// Service Factory Node
  /// </summary>
  public interface IServiceContainerNode<TService> : INode
    where TService : class
  {
    /// <summary>
    /// Get instance from the service factory of <typeparamref name="TService"/>
    /// </summary>
    /// <param name="data">Optional data</param>
    /// <returns>Instance of <typeparamref name="TService"/></returns>
    Task<TService> GetInstance(string data);
  }
}
