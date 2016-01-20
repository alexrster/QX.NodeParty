using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  public class SwitchArgumentBinding : CommandLineArgumentBinding<bool>
  {
    public static readonly string[] TrueStrings = {"1", "true", "+", string.Empty};

    public SwitchArgumentBinding(PropertyInfo propertyInfo, CommandLineArgumentAttribute argumentInfo) 
      : base(propertyInfo, argumentInfo)
    { }

    protected override bool ConvertValues(IEnumerable<string> values)
    {
      return values != null ? values.All(x => TrueStrings.Contains(x, StringComparer.InvariantCultureIgnoreCase)) : false;
    }
  }
}
