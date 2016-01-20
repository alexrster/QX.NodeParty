using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  public static class CommandLineArgumentExtensions
  {
    public static CommandLineArgumentBinding CreateBinding(this PropertyInfo propertyInfo)
    {
      Debug.Print("Binding command line argument definition '{0}'");
      return propertyInfo.GetCustomAttributes<CommandLineArgumentAttribute>()
        .Where(x => x != null)
        .Select(x => x.CreateArgumentBinding(propertyInfo))
        .FirstOrDefault(x => x != null);
    }
  }

  public class StringSingleArgAttribute : CommandLineArgumentAttribute
  {
    public StringSingleArgAttribute(string name, string description) : base(name, description)
    { }

    public override CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo)
    {
      return new SingleValueArgumentBinding<string>(propertyInfo, this, x => x);
    }
  }

  public class StringMultiArgAttribute : CommandLineArgumentAttribute
  {
    public StringMultiArgAttribute(string name, string description) : base(name, description)
    { }

    public override CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo)
    {
      return new MultiValueArgumentBinding<string>(propertyInfo, this, x => x);
    }
  }

  public class IntSingleArgAttribute : CommandLineArgumentAttribute
  {
    public IntSingleArgAttribute(string name, string description) : base(name, description)
    { }

    public override CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo)
    {
      return new SingleValueArgumentBinding<int>(propertyInfo, this, int.Parse);
    }
  }

  public class IntMultiArgAttribute : CommandLineArgumentAttribute
  {
    public IntMultiArgAttribute(string name, string description) : base(name, description)
    { }

    public override CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo)
    {
      return new MultiValueArgumentBinding<int>(propertyInfo, this, int.Parse);
    }
  }

  public class SwitchArgAttribute : CommandLineArgumentAttribute
  {
    public SwitchArgAttribute(string name, string description) : base(name, description)
    { }

    public override CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo)
    {
      return new SwitchArgumentBinding(propertyInfo, this);
    }
  }
}
