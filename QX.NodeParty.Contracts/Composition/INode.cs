namespace QX.NodeParty.Composition
{
  /// <summary>
  /// Smallest addressable unit 
  /// </summary>
  public interface INode
  {
    /// <summary>
    /// Unique Node URI
    /// </summary>
    NodeUri NodeUri { get; }
  }
}
