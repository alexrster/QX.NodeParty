using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace QX.NodeParty.Runtime.Bootstrap
{
  class FullFrameworkObjectFactoryProvider : IObjectFactoryProvider
  {
    public bool IgnoreCase { get; set; }

    public Func<object> GetObjectFactory(string id)
    {
      Debug.Print("Load Type from Name '{0}', ignoring case: {1}", id, IgnoreCase);
      var type = Type.GetType(id, false, IgnoreCase);

      Debug.Print("Get default (public parameterless) constructor of Type '{0}'", type);
      var ctor = type.GetConstructor(new Type[0]);
      if (ctor == null)
      {
        return null;
      }

      Debug.Print("Compose expression to create new instance using default .ctor of '{0}'", type);
      var ctorExpr = Expression.New(ctor);
      var convertExpr = Expression.Convert(ctorExpr, typeof (object));
      var factoryExpr = Expression.Lambda<Func<object>>(convertExpr);

      Debug.Print("Compile expression '{0}'", factoryExpr);
      return factoryExpr.Compile();
    }
  }
}
