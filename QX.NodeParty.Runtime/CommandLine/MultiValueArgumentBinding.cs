using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  public class MultiValueArgumentBinding<T> : CommandLineArgumentBinding<IEnumerable<T>>
  {
    private readonly Func<string, T> m_converterFunc;

    public MultiValueArgumentBinding(PropertyInfo propertyInfo, CommandLineArgumentAttribute argumentInfo, Func<string, T> converterFunc)
      : base(propertyInfo, argumentInfo)
    {
      Debug.Assert(propertyInfo != null);
      Debug.Assert(argumentInfo != null);
      Debug.Assert(converterFunc != null);

      m_converterFunc = converterFunc;
    }

    protected override IEnumerable<T> ConvertValues(IEnumerable<string> values)
    {
      return values != null ? values.Select(m_converterFunc) : Enumerable.Empty<T>();
    }
  }
}
