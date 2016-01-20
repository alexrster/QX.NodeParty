namespace QX.NodeParty.Composition.Bootstrap
{
  public static class BootstrapExtensions
  {
    public static T CreateObject<T>(this INodeRuntime runtime, string objectFactoryId) where T : class
    {
      return (T) runtime.CreateObject(objectFactoryId);
    }
  }
}
