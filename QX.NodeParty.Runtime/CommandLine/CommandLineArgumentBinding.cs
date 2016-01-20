using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  public abstract class CommandLineArgumentBinding
  {
    public PropertyInfo PropertyInfo { get; private set; }
    public CommandLineArgumentAttribute ArgumentInfo { get; private set; }

    protected CommandLineArgumentBinding(PropertyInfo propertyInfo, CommandLineArgumentAttribute argumentInfo)
    {
      PropertyInfo = propertyInfo;
      ArgumentInfo = argumentInfo;
    }

    protected abstract void UpdateModelValues(object model, IEnumerable<string> values);

    public T Bind<T>(T model, NameValueCollection arguments)
      where T : class, new()
    {
      if (model == null) model = new T();
      UpdateModelValues(model, ArgumentInfo.GetValues(arguments));

      return model;
    }
  }

  public abstract class CommandLineArgumentBinding<TValue> : CommandLineArgumentBinding
  {
    private readonly Action<object, TValue> _updateAction;

    protected CommandLineArgumentBinding(PropertyInfo propertyInfo, CommandLineArgumentAttribute argumentInfo)
      : base(propertyInfo, argumentInfo)
    {
      _updateAction = (x, v) => PropertyInfo.SetValue(x, v);
    }

    protected abstract TValue ConvertValues(IEnumerable<string> values);

    protected override void UpdateModelValues(object model, IEnumerable<string> values)
    {
      _updateAction(model, ConvertValues(values));
    }
  }
}
