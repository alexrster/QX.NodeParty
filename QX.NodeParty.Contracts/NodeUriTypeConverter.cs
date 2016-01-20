using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace QX.NodeParty
{
  public class NodeUriTypeConverter : UriTypeConverter
  {
    private static string TypeName = typeof(NodeUri).FullName;

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value == null)
      {
        var message = string.Format("Cannot convert null to '{0}'", TypeName);
        Debug.Fail(message);
        throw new ArgumentNullException("value", message);
      }

      if (value is string)
      {
        return new NodeUri((string)value, UriKind.RelativeOrAbsolute);
      }
      else if (value is Uri)
      {
        return new NodeUri((Uri) value);
      }

      var message2 = string.Format("Cannot convert null to '{0}'", TypeName);
      Debug.Fail(message2);
      throw new ArgumentNullException("value", message2);
    }

    public override bool IsValid(ITypeDescriptorContext context, object value)
    {
      return (value as string ?? string.Empty).StartsWith(NodeUri.UriScheme + Uri.SchemeDelimiter) && base.IsValid(context, value);
    }
  }
}
