using System.Threading.Tasks;

namespace QX.NodeParty.Services
{
  /// <summary>
  /// Service provider
  /// </summary>
  public interface IServiceLocator
  {
    /// <summary>
    /// Locate a <see cref="IServiceContainerNode{TService}"/> Node and get service  <typeparamref name="TService" /> instance
    /// </summary>
    /// <typeparam name="TService">Type of target service</typeparam>
    /// <param name="data">Optional data</param>
    /// <returns>Instance of service or NULL if not found</returns>
    Task<TService> GetService<TService>(string data = null) where TService : class;
  }
}
