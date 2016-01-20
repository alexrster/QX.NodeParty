using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  public class SingleValueArgumentBinding<T> : CommandLineArgumentBinding<T>
  {
    private readonly Func<string, T> m_converterFunc;

    public SingleValueArgumentBinding(PropertyInfo propertyInfo, CommandLineArgumentAttribute argumentInfo, Func<string, T> converterFunc)
      : base(propertyInfo, argumentInfo)
    {
      Debug.Assert(propertyInfo != null);
      Debug.Assert(argumentInfo != null);
      Debug.Assert(converterFunc != null);

      m_converterFunc = converterFunc;
    }

    protected override T ConvertValues(IEnumerable<string> values)
    {
      return values != null ? ArgumentInfo.IsRequired ? m_converterFunc(values.Single()) : m_converterFunc(values.First()) : default(T);
    }
  }
}
